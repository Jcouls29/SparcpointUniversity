using SparcpointUniversity.Sql.Abstractions;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SparcpointUniversity.Console
{
    public class DapperSqlProductRepository : IProductRepository
    {
        private readonly ISqlExecutor _Executor;

        public DapperSqlProductRepository(ISqlExecutor executor)
        {
            _Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public async Task<int> AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // NOTE: Additional checks should be performed on the product properties
            // before attempting to insert into SQL

            const string SQL = @"
                INSERT INTO [dbo].[Products] ([Name], [Description])
                VALUES (@Name, @Description)
                ;
                DECLARE @ProductId INT = SCOPE_IDENTITY();
                ;
                INSERT INTO [dbo].[ProductAttributes] ([ProductId], [Key], [Value])
                SELECT @ProductId, [Key], [Value]
                FROM @ProductAttributes
                ;
                SELECT @ProductId
                ;
            ";

            if (product.Attributes == null)
                product.Attributes = new System.Collections.Generic.Dictionary<string, string>();

            return await _Executor
                .WithTransaction()
                .ExecuteAsync(async (sqlConn, sqlTrans) =>
                {
                    return await sqlConn.QuerySingleAsync<int>(SQL, new
                    {
                        product.Name,
                        Description = product.Description ?? "",
                        ProductAttributes = product.Attributes.ToSqlParameter()
                    }, sqlTrans);
                });
        }
    }
}
