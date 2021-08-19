using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SparcpointUniversity.Sql.Abstractions;
using SparcpointUniversity.Sql.SqlServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparcpointUniversity.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            // Configure IoC Container as if we were using a Web API (startup.cs / ConfigureServices)
            IServiceProvider provider = Configure(config);
            IProductRepository repository = provider.GetRequiredService<IProductRepository>();

            // Insert Products
            int productAId = await repository.AddProductAsync(new Product
            {
                Name = "Playing Cards",
                Description = "A standard 52-card deck of playing cards",
                Attributes = new Dictionary<string, string>
                {
                    { "Color", "Red" },
                    { "Size", "Standard" },
                    { "Brand", null }
                }
            });
            System.Console.WriteLine($"Product A Saved! [Id = {productAId}]");

            int productBId = await repository.AddProductAsync(new Product
            {
                Name = "Microphone",
                Description = null,
                Attributes = null
            });
            System.Console.WriteLine($"Product B Saved! [Id = {productBId}]");

            // Let's wait for the enter key before exiting the application.
            // This allows us to see the identifiers for testing purposes.
            System.Console.WriteLine("Press Enter to quit...");
            System.Console.ReadLine();
        }

        static IServiceProvider Configure(IConfiguration config)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSqlServerExecutor("Server=localhost;Database=MyDatabase;Integrated Security=True;");

            var monitoringEnabledValue = config["Monitoring:SqlExecutor"];
            if (bool.TryParse(monitoringEnabledValue, out bool isMonitoringEnabled) && isMonitoringEnabled)
                services.AddPerformanceMonitoring((elapsed) => System.Console.Write($"[Elapsed Time: {elapsed}] "));

            services.AddSingleton<IProductRepository, SqlProductRepository>();

            return services.BuildServiceProvider();
        }
    }
}
