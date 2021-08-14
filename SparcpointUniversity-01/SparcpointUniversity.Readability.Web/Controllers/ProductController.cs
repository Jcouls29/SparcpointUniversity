using Microsoft.AspNetCore.Mvc;
using SparcpointUniversity.Readability.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparcpointUniversity.Readability.Web.Controllers
{
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProductListingRepository _ProductListingRepository;
        private readonly IProductInventoryRepository _ProductInventoryRepository;

        public ProductController(IProductListingRepository productListingRepository, IProductInventoryRepository productInventoryRepository)
        {
            _ProductListingRepository = productListingRepository ?? throw new ArgumentNullException(nameof(productListingRepository));
            _ProductInventoryRepository = productInventoryRepository ?? throw new ArgumentNullException(nameof(productInventoryRepository));
        }

        [HttpGet] 
        public IActionResult GetAllProductListings()
        {
            return Ok(_ProductListingRepository.GetAllProductListings());
        }

        [HttpGet("{productId}")] 
        public IActionResult GetProductListingById(int productId)
        {
            Product foundProduct = _ProductListingRepository.GetProductListing(productId);
            if (foundProduct == null)
                return NotFound();

            return Ok(foundProduct);
        }

        [HttpGet("{productId}/inventory-count")] 
        public IActionResult GetProductInventoryCount(int productId)
        {
            ProductInventoryEntry foundProductCounts = _ProductInventoryRepository.GetInventory(productId);
            if (foundProductCounts == null)
                return NotFound();

            return Ok(foundProductCounts);
        }

        [HttpPut("{productId}/inventory-count")]
        public IActionResult AdjustProductInventory(int productId, int onHandQuantityChanged)
        {
            try
            {
                return Ok(_ProductInventoryRepository.AdjustOnHandQuantity(productId, onHandQuantityChanged));
            } catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
