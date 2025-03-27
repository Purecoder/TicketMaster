using Newtonsoft.Json;
namespace TicketMaster.Obilet.Mvc.Models.Api
{
    public class SessionRequest
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("connection", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Connection Connection { get; set; }

        [JsonProperty("application", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Application Application { get; set; }

        [JsonProperty("browser", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Browser Browser { get; set; }
    }

    public class Connection
    {
        [JsonProperty("ip-address")]
        public string IpAddress { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }
    }

    public class Application
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("equipment-id")]
        public string EquipmentId { get; set; }
    }

    public class Browser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
