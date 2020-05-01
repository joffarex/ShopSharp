using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.ViewModels;

namespace ShopSharp.UI.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<CartViewModel> Cart { get; set; }

        public IActionResult OnGet([FromServices] GetCart getCart)
        {
            Cart = getCart.Exec();

            return Page();
        }
    }
}