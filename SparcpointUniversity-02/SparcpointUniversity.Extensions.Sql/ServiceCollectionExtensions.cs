using Microsoft.Extensions.DependencyInjection;
using SparcpointUniversity.Sql.Abstractions;
using SparcpointUniversity.Sql.SqlServer;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerExecutor(this IServiceCollection services, string connectionString)
            => services.AddSingleton<ISqlExecutor>(new SqlServerSqlExecutor(connectionString));

        public static IServiceCollection AddPerformanceMonitoring(this IServiceCollection services, Action<TimeSpan> callback)
        {
            services.Decorate<ISqlExecutor, PerformanceMonitoringSqlExecutor>();
            services.AddSingleton(new PerformanceMonitoringSqlExecutor.OnMonitorComplete(callback));
            return services;
        }
    }
}
