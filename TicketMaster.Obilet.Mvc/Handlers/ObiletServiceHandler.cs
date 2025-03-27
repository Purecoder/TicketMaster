using Microsoft.Extensions.Options;
using TicketMaster.OBilet.Mvc.Settings;

namespace TicketMaster.OBilet.Mvc.Handlers
{
    public class ObiletServiceHandler : DelegatingHandler
    {
        private readonly ObiletApiSettings _settings;

        public ObiletServiceHandler(IOptions<ObiletApiSettings> settings)
        {
            _settings = settings.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {_settings.Token}");
            request.Headers.TryAddWithoutValidation("Content-Type", "application/json");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
