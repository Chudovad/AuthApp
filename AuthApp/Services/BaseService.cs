using AuthApp.Models;
using AuthApp.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static AuthApp.StaticData;

namespace AuthApp.Services
{
    internal class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;

        public BaseService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ApiResponse> SendAsync(ApiRequest apiRequest)
        {
            try
            {
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage apiResponse = null;

                switch (apiRequest.ApiType)
                {
                    case ApiTypes.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiTypes.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiTypes.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await _httpClient.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<ApiResponse>(apiContent);
                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse
                {
                    Code = 1,
                    Type = "error",
                    Message = ex.Message.ToString()
                };
                return response;
            }
        }
    }
}
