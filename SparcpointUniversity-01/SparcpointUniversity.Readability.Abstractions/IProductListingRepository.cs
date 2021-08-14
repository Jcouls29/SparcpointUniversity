using System.Collections.Generic;

namespace SparcpointUniversity.Readability.Abstractions
{
    public interface IProductListingRepository
    {
        IEnumerable<Product> GetAllProductListings();
        Product GetProductListing(int productId);
    }
}
