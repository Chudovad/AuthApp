using AuthApp.Models;
using AuthApp.Services.Interfaces;
using AuthApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Windows.Input;

namespace AuthApp.ViewModels
{
    public partial class AuthViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string textInfo;

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public AuthViewModel(IAuthService authService, IServiceProvider serviceProvider)
        {
            _authService = authService;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            RegisterCommand = new RelayCommand(Register);
            _serviceProvider = serviceProvider;
        }

        private async Task LoginAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    TextInfo = "\"Имя пользователя\" и \"Пароль\"\rобязательные поля";
                    return;
                }
                ApiResponse response = await _authService.LoginAsync(UserName, Password);
                if (response != null && response.Code == 200)
                {
                    var responseUser = await _authService.GetUserAsync(UserName);
                    if (responseUser != null && responseUser.Code == 200)
                    {
                        var view1ViewModel = _serviceProvider.GetRequiredService<View1ViewModel>();
                        var mainViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

                        mainViewModel.AuthView = _serviceProvider.GetRequiredService<LogoutViewModel>();
                        view1ViewModel.IsAuthorized = true;
                        view1ViewModel.UserInfo = JsonConvert.DeserializeObject<User>(responseUser.Message);
                        view1ViewModel.UpdateView();
                        return;
                    }
                    TextInfo = responseUser.Message;
                    return;
                }
                TextInfo = response.Message;
            }
            catch (Exception ex)
            {
                TextInfo = ex.Message;
            }
        }

        private void Register()
        {
            var registerWindow = _serviceProvider.GetRequiredService<RegisterWindow>();
            var mainViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

            registerWindow.ShowInTaskbar = false;
            bool? result = registerWindow.ShowDialog();

            if (result == true)
            {
                mainViewModel.AuthView = _serviceProvider.GetRequiredService<LogoutViewModel>();
            }
        }
    }
}
