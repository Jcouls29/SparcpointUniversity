using System;
using System.Data;
using System.Threading.Tasks;

namespace SparcpointUniversity.Sql.Abstractions
{
    public interface ISqlExecutor
    {
        Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor);
    }
}
