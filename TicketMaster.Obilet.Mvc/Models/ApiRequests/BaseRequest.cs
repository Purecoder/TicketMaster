using Newtonsoft.Json;
namespace TicketMaster.Obilet.Mvc.Models.Api
{
    public class BaseRequest<T>
    {
        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } = "tr-TR";

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
