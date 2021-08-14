using SparcpointUniversity.Readability.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SparcpointUniversity.Readability.InMemory
{
    public class InMemoryProductListingRepository : IProductListingRepository
    {
        private readonly List<Product> _Products;

        public InMemoryProductListingRepository(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            _Products = new List<Product>(products);
        }

        public IEnumerable<Product> GetAllProductListings()
            => _Products;

        public Product GetProductListing(int productId)
            => _Products.FirstOrDefault(p => p.Id == productId);
    }
}
