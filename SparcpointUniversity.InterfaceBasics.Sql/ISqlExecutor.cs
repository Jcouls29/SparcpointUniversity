using System;
using System.Data;
using System.Threading.Tasks;

namespace SparcpointUniversity.InterfaceBasics.Sql.Abstractions
{
    public interface ISqlExecutor
    {
        T Execute<T>(Func<IDbConnection, IDbTransaction, T> executor);
        void Execute(Action<IDbConnection, IDbTransaction> executor);

        Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor);
        Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> executor);
    }


}
