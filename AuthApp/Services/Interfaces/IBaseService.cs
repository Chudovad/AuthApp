using AuthApp.Models;

namespace AuthApp.Services.Interfaces
{
    public interface IBaseService
    {
        Task<ApiResponse> SendAsync(ApiRequest apiRequest);
    }
}
