using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using TicketMaster.Obilet.Mvc.Models;
using TicketMaster.Obilet.Mvc.Models.Api;
using UAParser;

namespace TicketMaster.Obilet.Mvc.Controllers
{

    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected DeviceSession GetDeviceSession()
        {
            var sessionId = Request.Cookies["sessionId"];
            var deviceId = Request.Cookies["deviceId"];

            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId))
            {
                sessionId = HttpContext.Items["sessionId"]?.ToString();
                deviceId = HttpContext.Items["deviceId"]?.ToString();

                if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId))
                    return null;
            }

            return new DeviceSession
            {
                SessionId = sessionId,
                DeviceId = deviceId
            };
        }
    }
}
