using Newtonsoft.Json;
namespace TicketMaster.Obilet.Mvc.Models.Api
{
    public class JourneyRequest
    {
        [JsonProperty("origin-id")]
        public int OriginId { get; set; }

        [JsonProperty("destination-id")]
        public int DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public DateTime DepartureDate { get; set; }
    }
    public class JourneyRequestView
    {
        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}