using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Products;
using ShopSharp.Application.Products.ViewModels;

namespace ShopSharp.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GetProducts _getProducts;

        public IndexModel(GetProducts getProducts)
        {
            _getProducts = getProducts;
        }

        [BindProperty] public IEnumerable<ProductViewModel> Products { get; private set; }

        public void OnGet()
        {
            Products = _getProducts.Exec();
        }
    }
}