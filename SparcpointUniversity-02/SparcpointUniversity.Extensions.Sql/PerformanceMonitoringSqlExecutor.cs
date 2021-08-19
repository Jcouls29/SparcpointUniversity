using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SparcpointUniversity.Sql.Abstractions
{
    internal class PerformanceMonitoringSqlExecutor : ISqlExecutor
    {
        public delegate void OnMonitorComplete(TimeSpan elapsed);
        private readonly ISqlExecutor _InnerExecutor;
        private readonly OnMonitorComplete _Callback;

        public PerformanceMonitoringSqlExecutor(ISqlExecutor innerExecutor, OnMonitorComplete callback)
        {
            _InnerExecutor = innerExecutor ?? throw new ArgumentNullException(nameof(innerExecutor));
            _Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor)
        {
            Stopwatch watch = Stopwatch.StartNew();
            T results = await _InnerExecutor.ExecuteAsync(executor);

            watch.Stop();
            _Callback(watch.Elapsed);

            return results;
        }
    }
}
