using SparcpointUniversity.Sql.Abstractions;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SparcpointUniversity.Console
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly ISqlExecutor _Executor;

        public SqlProductRepository(ISqlExecutor executor)
        {
            _Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public async Task<int> AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // NOTE: Additional checks should be performed on the product properties
            // before attempting to insert into SQL

            const string SQL_PRODUCT = @"
                INSERT INTO [dbo].[Products] ([Name], [Description])
                VALUES (@Name, @Description)
                ;
                SELECT CAST(SCOPE_IDENTITY() AS INT)
                ;
            ";

            const string SQL_ATTR = @"
                INSERT INTO [dbo].[ProductAttributes] ([ProductId], [Key], [Value])
                VALUES (@ProductId, @Key, @Value)
                ;
            ";

            return await _Executor
                .WithTransaction()
                .ExecuteAsync(async (sqlConn, sqlTrans) =>
                {
                    int productId = await sqlConn.QuerySingleAsync<int>(SQL_PRODUCT, new
                    {
                        product.Name,
                        Description = product.Description ?? ""
                    }, sqlTrans);

                    if (product.Attributes?.Any() ?? false)
                    {
                        foreach (var keyValue in product.Attributes)
                        {
                            await sqlConn.ExecuteAsync(SQL_ATTR, new
                            {
                                ProductId = productId,
                                Key = keyValue.Key,
                                Value = keyValue.Value
                            }, sqlTrans);
                        }
                    }

                    return productId;
                });
        }
    }
}
