using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Gateway.Api
{
    public interface IApiClient<TResponse>
    {
        Task<TResponse> Get(string relativePath, string clientName, IDictionary<string, string>? queryStringParams = null);
    }

    public class ApiClient<TResponse> : IApiClient<TResponse>
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TResponse> Get(string relativePath, string clientName, IDictionary<string, string>? queryStringParams = null)
        {
            var client = _clientFactory.CreateClient(clientName);

            var endpoint = GetEndpoint(relativePath, queryStringParams);
            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var httpResponse = await client.SendAsync(message);
            var response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiClientException(httpResponse.StatusCode, response);

            return JsonConvert.DeserializeObject<TResponse>(response) ?? throw new Exception($"Could not deserialize object of type {typeof(TResponse).Name}");
        }

        private Uri GetEndpoint(string relativePath, IDictionary<string, string>? queryStringParams)
        {
            var endpoint = new Uri(relativePath);

            if (queryStringParams is null)
                return endpoint;

            var uriBuilder = new UriBuilder(endpoint);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var param in queryStringParams)
                query[param.Key] = param.Value;

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
