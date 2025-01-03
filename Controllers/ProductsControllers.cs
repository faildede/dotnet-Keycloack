using Microsoft.AspNetCore.Mvc;
using eshop_auth.Dal;
using eshop_auth.Models;
using Microsoft.AspNetCore.Authorization;

namespace eshop_auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productRepository;

        public ProductsController(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }
     
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [AllowAnonymous]
        [HttpGet("title/{title}")]
        public IActionResult GetByName(string title)
        {
            var product = _productRepository.GetProductByName(title);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with title '{title}' not found." });
            }
            return Ok(product); 
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}
