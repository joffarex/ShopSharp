using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.UI.Pages
{
    public class CartModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CartViewModel> Cart { get; set; }
        public string TotalValue { get; set; }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _context).Exec();
            var totalValue = Cart.Sum(x => x.RealValue * x.Quantity);
            TotalValue = $"${totalValue}";

            return Page();
        }
    }
}