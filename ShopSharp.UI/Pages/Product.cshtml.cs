using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Products;
using ShopSharp.Application.Products.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.UI.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] public CartDto CartDto { get; set; }

        public ProductViewModel Product { get; set; }

        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_context).Exec(name.Replace("-", " "));
            if (Product == null) return RedirectToPage("Index");
            return Page();
        }

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Exec(CartDto);

            return RedirectToPage("Cart");
        }
    }
}