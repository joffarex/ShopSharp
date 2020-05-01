using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Database;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                Quantity = 1
            };

            var result = await new AddToCart(HttpContext.Session, _context).ExecAsync(cartDto);

            if (result) return Ok("Item added to cart");

            return BadRequest("Failed to add to cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                Quantity = 1
            };

            var result = await new RemoveFromCart(HttpContext.Session, _context).ExecAsync(cartDto);

            if (result) return Ok("Item removed from cart");

            return BadRequest("Failed to remove from cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                All = true
            };

            var result = await new RemoveFromCart(HttpContext.Session, _context).ExecAsync(cartDto);

            if (result) return Ok("Item removed all cart");

            return BadRequest("Failed to remove all cart");
        }
    }
}