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
        private readonly GetProduct _getProduct;

        public ProductModel(ApplicationDbContext context, GetProduct getProduct)
        {
            _context = context;
            _getProduct = getProduct;
        }

        [BindProperty] public CartDto CartDto { get; set; }

        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(string name)
        {
            Product = await _getProduct.ExecAsync(name.Replace("-", " "));
            if (Product == null) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var stockAdded = await new AddToCart(HttpContext.Session, _context).ExecAsync(CartDto);

            if (stockAdded) return RedirectToPage("Cart");

            return Page(); // TODO: add warning that stock is on hold 
        }
    }
}