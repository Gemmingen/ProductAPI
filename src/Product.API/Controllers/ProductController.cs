using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges();

            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("search/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var productInDb = _context.Products.Find(id);
            if (productInDb == null)
            {
                return NotFound();
            }
            productInDb.Name = product.Name;
            productInDb.Price = product.Price;

            _context.SaveChanges();

            return Ok();
        }
    }
}
