using TicketMaster.Obilet.Mvc.Models;
using TicketMaster.Obilet.Mvc.Models.Api;

public interface IObiletSessionApiService
{
    Task<DeviceSession> GetSessionAsync(SessionRequest sessionRequest);
}
