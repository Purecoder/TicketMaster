using Newtonsoft.Json;
namespace TicketMaster.Obilet.Mvc.Models
{
    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}