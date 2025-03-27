using Microsoft.AspNetCore.Mvc;
using TicketMaster.Obilet.Mvc.Controllers;
using TicketMaster.Obilet.Mvc.Models;
using TicketMaster.Obilet.Mvc.Models.Api;
using UAParser;

[Route("bus")]
public class BusController : BaseController
{
    private readonly IObiletBusApiService _apiBusService;
    private readonly IObiletSessionApiService _apiSessionService;

    public BusController(IObiletBusApiService apiBusService, IObiletSessionApiService apiSessionService, ILogger<BaseController> logger) : base(logger)
    {
        _apiBusService = apiBusService;
        _apiSessionService = apiSessionService;
    }

    [Route("")]
    [HttpGet("index")]
    public async Task<IActionResult> Welcome()
    {
        _logger.LogInformation("Bus Index page visited");
        BaseRequest<string> req = new BaseRequest<string>
        {
            Data = null,
            Date = DateTime.Now,
            DeviceSession = GetDeviceSession(),
            Language = "tr-TR"
        };
        var locations = await _apiBusService.GetBusLocationsAsync(req);
        return View("Index");
    }

    [HttpGet("locations/search")]
    public async Task<IActionResult> GetLocationNames([FromQuery] string query = "")
    {
        var locations = await GetLocationList(query);
        if (locations != null && locations.Count > 0)
            return Json(new { suggestions = locations.Select(s => new Location { Value = s.Name, Data = s.Id }).ToList() });
        return null;
    }

    private async Task<List<BusLocationResponse>> GetLocationList(string? locationName = null)
    {
        BaseRequest<string> req = new BaseRequest<string>
        {
            Data = locationName ?? null,
            Date = DateTime.Now,
            DeviceSession = GetDeviceSession(),
            Language = "tr-TR"
        };
        return await _apiBusService.GetBusLocationsAsync(req);
    }

    [HttpPost("journeys")]
    public async Task<IActionResult> GetJourneys(JourneyRequestView model)
    {
        var response = new BaseResponse<List<JourneyResponse>>();
        if (model == null || model.OriginId == default || model.DestinationId == default)
        {
            response.Message = "Lütfen başlangıç ve varış noktalarını doğru seçtiğinizden emin olun.";
            return Json(response);
        }

        if (model.DepartureDate.Date < DateTime.Now.Date)
        {
            response.Message = "Kalkış tarihi bugünün tarihinden küçük olamaz!";
            return Json(response);
        }

        if (model.DestinationId == model.OriginId)
        {
            response.Message = "Başlangıç ve Hedef lokasyon aynı olamaz!";
            return Json(response);
        }

        var request = new BaseRequest<JourneyRequest>
        {
            Data = new JourneyRequest
            {
                OriginId = model.OriginId,
                DestinationId = model.DestinationId,
                DepartureDate = model.DepartureDate
            },
            DeviceSession = GetDeviceSession(),
            Date = DateTime.Now,
            Language = "tr-TR"
        };

        try
        {
            var data = await _apiBusService.GetJourneysAsync(request);
            response.Data = data.OrderBy(o => o.Journey.Departure).ToList();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return PartialView("_JourneyPartialView", response);
    }
}
