using TicketMaster.Obilet.Mvc.Models;
using TicketMaster.Obilet.Mvc.Models.Api;

public interface IObiletBusApiService
{
    Task<List<BusLocationResponse>> GetBusLocationsAsync(BaseRequest<string> model);
    Task<List<JourneyResponse>> GetJourneysAsync(BaseRequest<JourneyRequest> request);
}
