namespace TicketMaster.Obilet.Mvc.Models.Api
{
    public class SessionResponse
    {
        public string SessionId { get; set; }
        public string DeviceId { get; set; }
        public object Affiliate { get; set; }
        public int DeviceType { get; set; }
        public object Device { get; set; }
        public string IpCountry { get; set; }
        public int CleanSessionId { get; set; }
        public int CleanDeviceId { get; set; }
        public object IpAddress { get; set; }
    }
}