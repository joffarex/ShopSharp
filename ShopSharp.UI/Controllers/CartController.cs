using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                Quantity = 1
            };

            var result = await addToCart.ExecAsync(cartDto);

            if (result) return Ok("Item added to cart");

            return BadRequest("Failed to add to cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                Quantity = 1
            };

            var result = await removeFromCart.ExecAsync(cartDto);

            if (result) return Ok("Item removed from cart");

            return BadRequest("Failed to remove from cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                All = true
            };

            var result = await removeFromCart.ExecAsync(cartDto);

            if (result) return Ok("Item removed all cart");

            return BadRequest("Failed to remove all cart");
        }
    }
}