using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TicketMaster.Obilet.Mvc.Models;
using TicketMaster.Obilet.Mvc.Models.Api;
using TicketMaster.Obilet.Mvc.Services;
using TicketMaster.OBilet.Mvc.Settings;

public class ObiletSessionApiService : IObiletSessionApiService
{

     private readonly ObiletHttpApiService _httpService;

    public ObiletSessionApiService(ObiletHttpApiService httpService)
    {
        _httpService = httpService;
    }
    public async Task<DeviceSession> GetSessionAsync(SessionRequest sessionRequest)
    {
        var ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList
            .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

        var response = await _httpService.SendRequestAsync<DeviceSession>("client/getsession", sessionRequest);

        return response;
    }

}
