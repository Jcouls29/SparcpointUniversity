using SparcpointUniversity.Readability.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SparcpointUniversity.Readability.InMemory
{
    public class InMemoryProductInventoryRepository : IProductInventoryRepository
    {
        private readonly List<ProductInventoryEntry> _ProductInventory;

        public InMemoryProductInventoryRepository(IEnumerable<ProductInventoryEntry> entries)
        {
            if (entries == null)
                throw new ArgumentNullException(nameof(entries));

            _ProductInventory = new List<ProductInventoryEntry>(entries);
        }

        public ProductInventoryEntry GetInventory(int productId)
            => _ProductInventory.FirstOrDefault(pc => pc.Product.Id == productId);

        public ProductInventoryEntry AdjustOnHandQuantity(int productId, int onHandQuantityChange)
        {
            ProductInventoryEntry foundProductCounts = _ProductInventory.FirstOrDefault(pc => pc.Product.Id == productId);
            if (foundProductCounts == null)
                throw new EntityNotFoundException("Product");

            foundProductCounts.OnHandQuantity += onHandQuantityChange;

            return foundProductCounts;
        }
    }
}
