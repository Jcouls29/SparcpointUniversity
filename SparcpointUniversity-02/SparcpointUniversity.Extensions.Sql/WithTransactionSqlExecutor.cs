using SparcpointUniversity.Sql.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SparcpointUniversity.Extensions.Sql
{
    internal class WithTransactionSqlExecutor : ISqlExecutor
    {
        private readonly ISqlExecutor _InnerExecutor;

        public WithTransactionSqlExecutor(ISqlExecutor innerExecutor)
        {
            _InnerExecutor = innerExecutor ?? throw new ArgumentNullException(nameof(innerExecutor));
        }

        public Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor)
        {
            return _InnerExecutor.ExecuteAsync(async (sqlConn, sqlTrans) =>
            {
                if (sqlTrans != null)
                    return await executor(sqlConn, sqlTrans);
                 
                using (sqlTrans = sqlConn.BeginTransaction())
                {
                    T result = await executor(sqlConn, sqlTrans);
                    sqlTrans.Commit();
                    return result;
                }
            });
        }
    }
}
