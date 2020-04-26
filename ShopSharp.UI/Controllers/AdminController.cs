using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.ProductsAdmin;
using ShopSharp.Application.ProductsAdmin.ViewModels;
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
        public IActionResult CreateProduct(ProductViewModel viewModel)
        {
            return Ok(new CreateProduct(_context).Exec(viewModel));
        }

        [HttpPut("products")]
        public IActionResult UpdateProduct(ProductViewModel viewModel)
        {
            return Ok(new UpdateProduct(_context).Exec(viewModel));
        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return Ok(new DeleteProduct(_context).Exec(id));
        }
    }
}