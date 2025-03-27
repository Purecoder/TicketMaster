using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TicketMaster.Obilet.Mvc.Models.Api;
using TicketMaster.OBilet.Mvc.Settings;

namespace TicketMaster.Obilet.Mvc.Services
{
    public class ObiletHttpApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ObiletHttpApiService> _logger;

        public ObiletHttpApiService(IHttpClientFactory httpClient, ILogger<ObiletHttpApiService> logger)
        {
            _httpClient = httpClient.CreateClient(nameof(ObiletApiSettings));
            _logger = logger;
        }

        public async Task<T> SendRequestAsync<T>(string endpoint, object request)
        {
            try
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                }), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, requestContent);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<BaseResponse<T>>(responseContent);

                if (apiResponse?.Status != "Success")
                {
                    _logger.LogError($"API error: {apiResponse.Message}, User message: {apiResponse.UserMessage}");
                    throw new Exception($"API error: {apiResponse.Message}");
                }

                return apiResponse.Data;
            }
            catch (Exception ex)
            {
                // Exception model doldurulabilir
                throw;
            }
        }
    }
}
