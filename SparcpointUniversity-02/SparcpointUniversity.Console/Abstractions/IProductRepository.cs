using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparcpointUniversity.Console
{
    public interface IProductRepository
    {
        Task<int> AddProductAsync(Product product);
    }
}
