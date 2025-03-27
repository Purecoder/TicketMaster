var setDatePicker = function (day = "today") {
    const dateTomorrow = new Date();
    dateTomorrow.setDate(new Date().getDate() + 1);
    if (day == "today") {
        $('#datePicker').datepicker("setDate", new Date());
    }
    else {
        $('#datePicker').datepicker("setDate", dateTomorrow);
    }
}

$(function () {
    var currentOrigin = getJsonCookie("origin");
    var currentDestination = getJsonCookie("destination");
    var currentDepartureDate = getCookie("departureDate");

    if (currentOrigin !== undefined)
        $('#fromLocation').val(currentOrigin.value).data("id", currentOrigin.dataId);

    if (currentOrigin !== undefined)
        $('#toLocation').val(currentDestination.value).data("id", currentDestination.dataId);



    $('#datePicker').datepicker({
        onSelect: function (date) {
            setCookie("departureDate", date, 30);
        }
    });

    if (currentDepartureDate !== undefined || currentDepartureDate != null) {

        try {
            var systemDate = new Date().toLocaleDateString();
            var currentDate = new Date(currentDepartureDate).toLocaleDateString();

            if (currentDate >= systemDate)
                $('#datePicker').datepicker("setDate", currentDate);
            else
                setDatePicker('tomorrow');

        } catch (error) {
            console.log(error);
            setDatePicker('tomorrow');
        }
    }
    else
        setDatePicker('tomorrow');



    // Switch locations
    $('#switchLocations').click(function () {
        var fromLocationData = { value: $('#fromLocation').val(), dataId: $('#fromLocation').data("id") };
        var toLocationData = { value: $('#toLocation').val(), dataId: $('#toLocation').data("id") };

        var tempLocationData = fromLocationData;
        fromLocationData = toLocationData;
        toLocationData = tempLocationData;

        $('#fromLocation').val(fromLocationData.value).data("id", fromLocationData.dataId);
        $('#toLocation').val(toLocationData.value).data("id", toLocationData.dataId);

        setJsonCookie("origin", fromLocationData);
        setJsonCookie("destination", toLocationData);

    });



    $('#fromLocation').devbridgeAutocomplete({
        serviceUrl: '/bus/locations/search',
        deferRequestBy: 500,
        onSelect: function (suggestion) {
            $('#fromLocation').data("id", suggestion.data);
            setJsonCookie("origin", { value: suggestion.value, dataId: suggestion.data });
            console.log('You selected: ' + suggestion.value + ', ' + suggestion.data);
        }
    });


    $('#toLocation').devbridgeAutocomplete({
        serviceUrl: '/bus/locations/search',
        deferRequestBy: 500,
        onSelect: function (suggestion) {
            $('#toLocation').data("id", suggestion.data);

            setJsonCookie("destination", { value: suggestion.value, dataId: suggestion.data });
            console.log('You selected: ' + suggestion.value + ', ' + suggestion.data);
        }
    });

    //
    $("#findTickets").click(function () {
        var dDate = new Date($('#datePicker').val())
        const [month, day, year] = [
            dDate.getMonth() + 1,
            dDate.getDate(),
            dDate.getFullYear(),
        ];
        //var formattedDate = new Date(year.toString(),month.toString(),day.toString());
        var formattedDate = year.toString() + "/" + month.toString() + "/" + day.toString();

        var data = {
            OriginId: $('#fromLocation').data("id"),
            DestinationId: $('#toLocation').data("id"),
            DepartureDate: formattedDate
        };

        $.ajax({
            url: "/bus/journeys",
            type: "POST",
            data: data,
            success: function (data) {
                if (typeof (data) != 'string' && data.data == null) {
                    alert(data.message);
                }
                $("#locationItems").html(data);
            },
            error: function (xhr) {
                //alert("Bir hata oluştu.");
                console.log("Hata: " + xhr.responseText);
            }
        });
    });


});

