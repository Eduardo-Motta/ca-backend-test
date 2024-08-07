using Nexer.Finance.Domain.Dtos;
using Nexer.Finance.Domain.ExternalApis;
using System.Net.Http.Json;

namespace Nexer.Finance.Infrastructure.ExternalApis
{
    public class BillingClient : IBillingClientApi
    {
        private readonly HttpClient _client;
        private readonly string _urlBase = "https://65c3b12439055e7482c16bca.mockapi.io/";

        public BillingClient()
        {
            _client = new HttpClient();

            CreateClient();
        }

        private void CreateClient()
        {
            _client.DefaultRequestHeaders.Add("Accept", "text/html;charset=utf-8");
            _client.BaseAddress = new Uri(_urlBase);
        }

        public async Task<List<BillingDto>> GetBillingAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _client.GetAsync("api/v1/billing");

            response.EnsureSuccessStatusCode();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException(response.ToString());
            }

            var result = await response.Content.ReadFromJsonAsync<List<BillingDto>>();

            if (result is null)
            {
                return new List<BillingDto>();
            }

            return result;
        }
    }
}
