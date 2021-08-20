using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SparcpointUniversity.Sql.Abstractions;
using SparcpointUniversity.Sql.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SparcpointUniversity.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RepoDb.SqlServerBootstrap.Initialize();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            // Configure IoC Container as if we were using a Web API (startup.cs / ConfigureServices)
            IServiceProvider provider = Configure(config);

            ISqlExecutor executor = provider.GetRequiredService<ISqlExecutor>();
            IProductRepository dapperRepository = new DapperSqlProductRepository(executor);
            IProductRepository repoDBRepository = new RepoDBSqlProductRepository(executor);

            const int LOOP_COUNT = 1000;

            TimeSpan dapperResults = await MeasureAverageTime(dapperRepository, LOOP_COUNT);
            TimeSpan repoDBResults = await MeasureAverageTime(repoDBRepository, LOOP_COUNT);

            System.Console.WriteLine($"Dapper (Avg): {dapperResults}, RepoDB (Avg): {repoDBResults}");

            // Let's wait for the enter key before exiting the application.
            // This allows us to see the identifiers for testing purposes.
            System.Console.WriteLine("Press Enter to quit...");
            System.Console.ReadLine();
        }

        private static async Task<TimeSpan> MeasureAverageTime(IProductRepository repository, int loopCount)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < loopCount; i++)
            {
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
            }

            return watch.Elapsed / loopCount;
        }

        static IServiceProvider Configure(IConfiguration config)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSqlServerExecutor("Server=localhost;Database=MyDatabase;Integrated Security=True;");

            var monitoringEnabledValue = config["Monitoring:SqlExecutor"];
            if (bool.TryParse(monitoringEnabledValue, out bool isMonitoringEnabled) && isMonitoringEnabled)
                services.AddPerformanceMonitoring((elapsed) => System.Console.Write($"[Elapsed Time: {elapsed}] "));

            services.AddSingleton<IProductRepository, DapperSqlProductRepository>();

            return services.BuildServiceProvider();
        }
    }
}
