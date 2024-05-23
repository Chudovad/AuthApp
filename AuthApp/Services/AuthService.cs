using AuthApp.Models;
using AuthApp.Services.Interfaces;

namespace AuthApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse> LoginAsync(string username, string password)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiTypes.GET,
                Url = StaticData.AuthAPIBase + $"/user/login?{nameof(username)}={username}&{nameof(password)}={password}"
            });
        }

        public async Task<ApiResponse> RegisterAsync(User user)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiTypes.POST,
                Data = user,
                Url = StaticData.AuthAPIBase + $"/user"
            });
        }

        public async Task<ApiResponse> LogoutAsync()
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiTypes.GET,
                Url = StaticData.AuthAPIBase + $"/user/logout"
            });
        }

        public async Task<ApiResponse> GetUserAsync(string username)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiTypes.GET,
                Url = StaticData.AuthAPIBase + $"/user/{username}"
            });
        }
    }
}
