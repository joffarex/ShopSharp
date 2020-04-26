using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Products;
using ShopSharp.Application.Products.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] public IEnumerable<ProductsViewModel> Products { get; private set; }

        public void OnGet()
        {
            Products = new GetProducts(_context).Exec();
        }
    }
}