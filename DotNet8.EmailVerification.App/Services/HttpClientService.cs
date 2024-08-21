using DotNet8.EmailVerification.Shared;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DotNet8.EmailVerification.App.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> ExecuteAsync<T>(string endpoint, EnumHttpMethod httpMethod, object? requestModel = null)
        {
            HttpResponseMessage? response = null;
            HttpContent? content = null;

            if (requestModel is not null)
            {
                string jsonStr = requestModel.ToJson();
                content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
            }

            switch (httpMethod)
            {
                case EnumHttpMethod.GET:
                    response = await _httpClient.GetAsync(endpoint);
                    break;
                case EnumHttpMethod.POST:
                    response = await _httpClient.PostAsync(endpoint, content);
                    break;
                case EnumHttpMethod.PUT:
                    response = await _httpClient.PutAsync(endpoint, content);
                    break;
                case EnumHttpMethod.PATCH:
                    response = await _httpClient.PatchAsync(endpoint, content);
                    break;
                case EnumHttpMethod.DELETE:
                    response = await _httpClient.DeleteAsync(endpoint);
                    break;
                case EnumHttpMethod.None:
                default:
                    throw new Exception("Invalid Http Method.");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return responseJson.ToObject<T>();
        }
    }

    public enum EnumHttpMethod
    {
        None,
        GET,
        POST,
        PUT,
        PATCH,
        DELETE
    }
}
