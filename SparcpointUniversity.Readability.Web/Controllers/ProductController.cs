using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparcpointUniversity.Readability.Web.Controllers
{
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private static List<Product> products;
        private static List<ProductCounts> counts;

        static ProductController()
        {
            products=new List<Product>{new Product{Id = 1,Name = "1-Year Membership Giftcard",Description="A one year membership to this channel."},new Product{Id = 2,Name = "Lifetime Membership Giftcard", Description = "A lifetime membership to this channel."}};
            counts=new List<ProductCounts>{new ProductCounts{Product=products[0],Count=15},new ProductCounts{Product=products[1],Count=6}};
        }
        [HttpGet] public IActionResult Index(){return Ok(products);}
        [HttpGet("{id}")] public IActionResult Get(int id)
        {
            for(int i=0;i<products.Count;i++) if(products[i].Id==id) return Ok(products[i]);
            return BadRequest();
        }
        [HttpGet("{id}/counts")] public IActionResult Counts(int id)
        {
            for(int i=0;i<counts.Count;i++)
            {
                if(counts[i].Product.Id==id) return Ok(counts[i]);
            }
            return BadRequest();
        }
        [HttpPost("{id}/counts/remove")] public IActionResult Remove1Count(int id){
            for (int i = 0; i < counts.Count; i++)
            {
                if (counts[i].Product.Id == id) counts[i].Count=counts[i].Count-1;
            }
            return Ok();
        }
        [HttpPost("{id}/counts/add")] public IActionResult Add1Count(int id) {
            for (int i = 0; i < counts.Count; i++)
            {
                if (counts[i].Product.Id == id) counts[i].Count=counts[i].Count+1;
            }
            return Ok(); }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProductCounts
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
