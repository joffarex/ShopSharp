using System.Linq;
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

        [HttpPost("{stockId}/{quantity}")]
        public async Task<IActionResult> Remove(int stockId, int quantity, [FromServices] RemoveFromCart removeFromCart)
        {
            var cartDto = new CartDto
            {
                StockId = stockId,
                Quantity = quantity
            };

            var result = await removeFromCart.ExecAsync(cartDto);

            if (result) return Ok("Item removed from cart");

            return BadRequest("Failed to remove from cart");
        }

        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Exec().Sum(x => x.RealValue * x.Quantity);
            return PartialView("Components/Cart/Small", $"${totalValue}");
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Exec();
            return PartialView("_CartPartial", cart);
        }
    }
}