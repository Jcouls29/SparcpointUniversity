using Microsoft.Extensions.DependencyInjection;
using SparcpointUniversity.Readability.Abstractions;
using SparcpointUniversity.Readability.InMemory;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleInMemoryRepositories(this IServiceCollection services)
        {
            var products = new List<Product> {
                new Product { Id = 1, Name = "1-Year Membership Giftcard", Description = "A one year membership to this channel." },
                new Product { Id = 2, Name = "Lifetime Membership Giftcard", Description = "A lifetime membership to this channel." }
            };
            services.AddSingleton<IProductListingRepository>(new InMemoryProductListingRepository(products));
            services.AddSingleton<IProductInventoryRepository>(new InMemoryProductInventoryRepository(new List<ProductInventoryEntry> {
                new ProductInventoryEntry { Product = products[0], OnHandQuantity = 15 },
                new ProductInventoryEntry { Product = products[1], OnHandQuantity = 6 }
            }));

            return services;
        }
    }
}
