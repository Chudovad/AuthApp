using AuthApp.Models;
using AuthApp.Services.Interfaces;
using AuthApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace AuthApp.ViewModels
{
    public partial class RegisterWindowViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private int userStatus;

        [ObservableProperty]
        private string textInfo;

        public ICommand RegisterCommand { get; }

        public RegisterWindowViewModel(IAuthService authService, IServiceProvider serviceProvider)
        {
            _authService = authService;
            RegisterCommand = new AsyncRelayCommand(RegisterAsync);
            _serviceProvider = serviceProvider;
        }

        private async Task RegisterAsync()
        {
            if (!string.IsNullOrEmpty(UserName) || !string.IsNullOrEmpty(Password))
            {
                User user = new User()
                {
                    UserName = UserName,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    Phone = Phone,
                    UserStatus = UserStatus
                };
                
                var view1ViewModel = _serviceProvider.GetRequiredService<View1ViewModel>();
                var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

                var result = await _authService.RegisterAsync(user);
                if (result.Code == 200)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault().DialogResult = true;
                        Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault()?.Close();
                    });

                    view1ViewModel.UserInfo = user;
                    view1ViewModel.IsAuthorized = true;
                    view1ViewModel.UpdateView();
                }
                else
                {
                    TextInfo = result.Message;
                }
            }
            else
            {
                TextInfo = "\"Имя пользователя\" и \"Пароль\" обязательные поля";
            }

        }
    }
}
