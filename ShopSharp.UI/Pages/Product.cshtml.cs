using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Products;
using ShopSharp.Application.Products.ViewModels;

namespace ShopSharp.UI.Pages
{
    public class ProductModel : PageModel
    {
        [BindProperty] public CartDto CartDto { get; set; }

        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(string name, [FromServices] GetProduct getProduct)
        {
            Product = await getProduct.ExecAsync(name.Replace("-", " "));
            if (Product == null) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var stockAdded = await addToCart.ExecAsync(CartDto);

            if (stockAdded) return RedirectToPage("Cart");

            return Page(); // TODO: add warning that stock is on hold 
        }
    }
}