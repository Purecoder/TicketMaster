using Microsoft.AspNetCore.Mvc.Filters;
using TicketMaster.Obilet.Mvc.Models.Api;
using UAParser;

namespace TicketMaster.Obilet.Mvc.MiddleWare
{
    public class ObiletSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public ObiletSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IObiletSessionApiService sessionApiService)
        {
            string sessionId = context.Request.Cookies["sessionId"];
            string deviceId = context.Request.Cookies["deviceId"];

            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId))
            {

                var ipAddress = context.Connection.RemoteIpAddress?.ToString();
                var port = context.Connection.RemotePort;
                var userAgent = context.Request.Headers["User-Agent"].ToString();

                var uaParser = Parser.GetDefault();
                var clientInfo = uaParser.Parse(userAgent);

                var browserName = clientInfo.UA.Family;
                var browserVersion = clientInfo.UA.Major;

                var sessionRequest = new SessionRequest()
                {
                    Type = 1,
                    Connection = new Connection()
                    {
                        IpAddress = ipAddress,
                        Port = port.ToString()
                    },
                    Browser = new Browser()
                    {
                        Name = browserName,
                        Version = browserVersion
                    }
                };

                var newSession = await sessionApiService.GetSessionAsync(sessionRequest);

                context.Response.Cookies.Append("sessionId", newSession.SessionId, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddDays(30)
                });

                context.Response.Cookies.Append("deviceId", newSession.DeviceId, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddDays(30)
                });

                 context.Items["sessionId"] = newSession.SessionId;
                 context.Items["deviceId"] = newSession.DeviceId;

            }

            await _next(context);
        }
    }
}
