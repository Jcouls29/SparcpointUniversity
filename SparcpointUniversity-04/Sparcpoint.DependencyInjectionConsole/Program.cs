// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();
IServiceProvider provider = host.Services;

CreateTransientServices(provider);
CreateScopedServices(provider);
CreateSingletonServices(provider);

await host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => Configure(services));

static void CreateTransientServices(IServiceProvider provider)
{
    Console.WriteLine("[Transient]");
    using var scope = provider.CreateScope();

    _ = scope.ServiceProvider.GetService<TransientServiceHost1>();
    _ = scope.ServiceProvider.GetService<TransientServiceHost2>();
}

static void CreateScopedServices(IServiceProvider provider)
{
    Console.WriteLine("[Scoped]");
    using var scope = provider.CreateScope();

    _ = scope.ServiceProvider.GetService<ScopedServiceHost1>();
    _ = scope.ServiceProvider.GetService<ScopedServiceHost2>();
}

static void CreateSingletonServices(IServiceProvider provider)
{
    Console.WriteLine("[Singleton]");
    using var scope = provider.CreateScope();

    _ = scope.ServiceProvider.GetService<SingletonServiceHost1>();
    _ = scope.ServiceProvider.GetService<SingletonServiceHost2>();
}

static IServiceCollection Configure(IServiceCollection services)
{
    services.AddSingleton<TransientServiceDependency>();
    services.AddTransient<TransientServiceHost1>();
    services.AddTransient<TransientServiceHost2>();

    services.AddScoped<ScopedServiceDependency>();
    services.AddScoped<ScopedServiceHost1>();
    services.AddScoped<ScopedServiceHost2>();

    services.AddSingleton<SingletonServiceDependency>();
    services.AddSingleton<SingletonServiceHost1>();
    services.AddSingleton<SingletonServiceHost2>();

    return services;
}

public class UserRepository
{
    public void CreateUser(string name)
    {
        // Create SQL Connection
        // Run Command
        // Dispose of Connection
    }
}

public class UserPreferencesService
{
    private readonly string _Username;

    public UserPreferencesService(string username)
    {
        _Username = username;
    }

    public void SavePreferences(string preferences)
    {
        // Create SQL Connection
        // Run Command w/ _Username
        // Dispose of Connection
    }
}

public class SqlManager
{
    public SqlManager(string connectionString)
    {

    }

    // Bunch of SQL Calls that can be made.

    public void Dispose()
    {
        // Dispose of the Connection
    }
}
