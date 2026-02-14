using FluentValidation;
using GeradorListaAssados.Desktop.Navegation;
using GeradorListaAssados.Desktop.ViewModels;
using GeradorListaAssados.Desktop.Windows;
using GeradorListaAssados.Engine.Context;
using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Repositories;
using GeradorListaAssados.Engine.Services;
using GeradorListaAssados.Engine.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using Application = System.Windows.Application;

namespace GeradorListaAssados.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host = null!;
        private IServiceScope _mainScope;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    ConfigureDatabase(services);
                    ConfigureRepositories(services);
                    ConfigureServices(services);
                    RegisterViewModels(services);
                    RegisterWindows(services);
                })
                .Build();

            _host.Start();

            ApplyDatabaseMigrations(_host);

            _mainScope = _host.Services.CreateScope();

            var mainWindow = _mainScope.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _mainScope?.Dispose();
            _host?.Dispose();
            base.OnExit(e);
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GeradorListaAssados",
                "app.db");

            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

            var connectionString = $"Data Source={dbPath}";

            services.AddDbContext<GeneratorDbContext>(options =>
                options.UseSqlite(connectionString)
            );
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavegationService, NavegationService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<AddProductViewModel>();
        }

        private static void RegisterWindows(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AddProductWindow>();
        }

        private static void ApplyDatabaseMigrations(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GeneratorDbContext>();
            db.Database.Migrate();
        }
    }
}
