using AuthApp.Models;
using AuthApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace AuthApp.ViewModels
{
    public partial class LogoutViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        public ICommand LogoutCommand { get; }

        public LogoutViewModel(IAuthService authService, IServiceProvider serviceProvider)
        {
            _authService = authService;
            LogoutCommand = new AsyncRelayCommand(LogoutAsync);
            _serviceProvider = serviceProvider;
        }

        private async Task LogoutAsync()
        {
            ApiResponse response = await _authService.LogoutAsync();
            if (response != null && response.Code == 200)
            {
                var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
                var view1ViewModel = _serviceProvider.GetRequiredService<View1ViewModel>();

                mainWindowViewModel.AuthView = _serviceProvider.GetRequiredService<AuthViewModel>();
                view1ViewModel.IsAuthorized = false;
                view1ViewModel.UpdateView();
            }
        }
    }
}
