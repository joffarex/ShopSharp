using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.ProductsAdmin;
using ShopSharp.Application.ProductsAdmin.Dto;
using ShopSharp.Database;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(new GetProducts(_context).Exec());
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new GetProduct(_context).Exec(id));
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            return Ok(await new CreateProduct(_context).Exec(productDto));
        }

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            return Ok(await new UpdateProduct(_context).Exec(id, productDto));
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await new DeleteProduct(_context).Exec(id));
        }
    }
}