using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.ProductsAdmin;
using ShopSharp.Application.ProductsAdmin.Dto;
using ShopSharp.Database;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetProducts()
        {
            return Ok(new GetProducts(_context).Exec());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new GetProduct(_context).Exec(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            return Ok(await new CreateProduct(_context).Exec(productDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            return Ok(await new UpdateProduct(_context).Exec(id, productDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await new DeleteProduct(_context).Exec(id));
        }
    }
}