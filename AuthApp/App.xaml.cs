using AuthApp.Services;
using AuthApp.Services.Interfaces;
using AuthApp.ViewModels;
using AuthApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AuthApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBaseService, BaseService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<View1ViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();

            services.AddTransient<AuthViewModel>();
            services.AddTransient<LogoutViewModel>();
            services.AddTransient<View2ViewModel>();
            services.AddTransient<RegisterWindowViewModel>();
            services.AddTransient<RegisterWindow>();

        }
    }

}
