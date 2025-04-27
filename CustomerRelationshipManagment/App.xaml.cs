using System.Configuration;
using System.Data;
using System.Windows;
using CustomerRelationshipManagment.Interfaces;
using CustomerRelationshipManagment.Repositories;
using CustomerRelationshipManagment.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerRelationshipManagment
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configure Logging
            services.AddLogging();

            // Register Services
            services.AddSingleton<RepositoryInterface, MainRepository>();

            // Register ViewModels
            services.AddSingleton<IMainViewModel, MainWindowViewModel>();

            // Register Views
            services.AddSingleton<MainWindow>();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            // Dispose of services if needed
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

}
