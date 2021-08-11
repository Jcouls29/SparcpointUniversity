using SparcpointUniversity.InterfaceBasics.Sql.Abstractions;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SparcpointUniversity.InterfaceBasics.Extensions.Sql
{
    internal class WithTransactionSqlExecutor : ISqlExecutor
    { 
        private readonly ISqlExecutor _InnerExecutor;
        private readonly IsolationLevel? _IsolationLevel;

        public WithTransactionSqlExecutor(ISqlExecutor executor)
        {
            _InnerExecutor = executor ?? throw new ArgumentNullException(nameof(executor));
            _IsolationLevel = null;
        }

        public WithTransactionSqlExecutor(ISqlExecutor executor, IsolationLevel isolationLevel)
        {
            _InnerExecutor = executor ?? throw new ArgumentNullException(nameof(executor));
            _IsolationLevel = isolationLevel;
        }

        public T Execute<T>(Func<IDbConnection, IDbTransaction, T> executor)
        {
            return _InnerExecutor.Execute<T>((dbConn, dbTrans) =>
            {
                if (dbTrans != null)
                    return executor(dbConn, dbTrans);

                using (IDbTransaction innerTrans = BeginTransaction(dbConn))
                {
                    T result = executor(dbConn, innerTrans);
                    dbTrans.Commit();
                    return result;
                }
            });
        }

        public void Execute(Action<IDbConnection, IDbTransaction> executor)
        {
            _InnerExecutor.Execute((dbConn, dbTrans) =>
            {
                if (dbTrans != null)
                {
                    executor(dbConn, dbTrans);
                    return;
                }

                using (IDbTransaction innerTrans = BeginTransaction(dbConn))
                {
                    executor(dbConn, innerTrans);
                    dbTrans.Commit();
                }
            });
        }

        public Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor)
        {
            return _InnerExecutor.ExecuteAsync<T>(async (dbConn, dbTrans) =>
            {
                if (dbTrans != null)
                    return await executor(dbConn, dbTrans);

                using (IDbTransaction innerTrans = BeginTransaction(dbConn))
                {
                    T result = await executor(dbConn, innerTrans);
                    dbTrans.Commit();
                    return result;
                }
            });
        }

        public Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> executor)
        {
            return _InnerExecutor.ExecuteAsync(async (dbConn, dbTrans) =>
            {
                if (dbTrans != null)
                {
                    await executor(dbConn, dbTrans);
                    return;
                }

                using (IDbTransaction innerTrans = BeginTransaction(dbConn))
                {
                    await executor(dbConn, innerTrans);
                    dbTrans.Commit();
                }
            });
        }

        private IDbTransaction BeginTransaction(IDbConnection connection)
        {
            if (_IsolationLevel == null)
                return connection.BeginTransaction();

            return connection.BeginTransaction(_IsolationLevel.Value);
        }
    }
}
