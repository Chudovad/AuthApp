using AuthApp.Models;

namespace AuthApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> LoginAsync(string username, string password);
        Task<ApiResponse> RegisterAsync(User user);
        Task<ApiResponse> LogoutAsync();
        Task<ApiResponse> GetUserAsync(string username);
    }
}
