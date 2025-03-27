using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TicketMaster.Obilet.Mvc.Models;
using TicketMaster.Obilet.Mvc.Models.Api;
using TicketMaster.Obilet.Mvc.Services;
using TicketMaster.OBilet.Mvc.Settings;

public class ObiletBusApiService : IObiletBusApiService
{
    private readonly ObiletHttpApiService _httpService;

    public ObiletBusApiService(ObiletHttpApiService httpService)
    {
        _httpService = httpService;
    }

    public async Task<List<BusLocationResponse>> GetBusLocationsAsync(BaseRequest<string> request)
    {
        var response = await _httpService.SendRequestAsync<List<BusLocationResponse>>("location/getbuslocations", request);
        return response;
    }

    public async Task<List<JourneyResponse>> GetJourneysAsync(BaseRequest<JourneyRequest> request)
    {
        var response = await _httpService.SendRequestAsync<List<JourneyResponse>>("journey/getbusjourneys", request);
        return response;
    }
}
