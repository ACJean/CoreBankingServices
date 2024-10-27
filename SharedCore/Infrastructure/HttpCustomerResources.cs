using Microsoft.Extensions.Logging;
using SharedOperations.Domain.Services;
using System.Net;
using System.Text.Json;

namespace SharedOperations.Infrastructure
{
    public class HttpCustomerResources : ICustomerResources
    {
        private readonly ILogger<HttpCustomerResources> _logger;
        private readonly HttpClient _client;

        public HttpCustomerResources(ILogger<HttpCustomerResources> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _client = httpClientFactory.CreateClient("CustomerService");
        }

        public async Task<string> GetName(string customerIdentity)
        {
            var response = await _client.GetAsync($"/customers/{customerIdentity}");

            string responseData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Falló la petición de consulta cliente {0}: Respuesta - {1}", customerIdentity, responseData);
                return null;
            }

            using JsonDocument doc = JsonDocument.Parse(responseData);

            JsonElement root = doc.RootElement;

            string customerName = root.GetProperty("name").GetString();

            if (customerName is null)
            {
                _logger.LogWarning("No hay datos para el campo 'name' {0}", customerIdentity);
            }

            return customerName;
        }

        public async Task<bool> IsExist(string customerIdentity)
        {
            var response = await _client.GetAsync($"/customers/{customerIdentity}");
            return response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK;
        }
    }
}
