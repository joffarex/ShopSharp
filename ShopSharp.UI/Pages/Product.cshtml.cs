using System.Threading.Tasks;
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

        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Exec(name.Replace("-", " "));
            if (Product == null) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var stockAdded = await new AddToCart(HttpContext.Session, _context).Exec(CartDto);

            if (stockAdded) return RedirectToPage("Cart");

            return Page(); // TODO: add warning that stock is on hold 
        }
    }
}