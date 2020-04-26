using System.Collections.Generic;
using System.Threading.Tasks;
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

        /*
         * BindProperty binds this model to cshtml's asp-for attributes in a way
         * that we dont need to use class name anymore
         */
        [BindProperty] public ProductViewModel Product { get; set; }
        [BindProperty] public IEnumerable<ProductViewModel> Products { get; private set; }

        public void OnGet()
        {
            Products = new GetProducts(_context).Exec();
        }

        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_context).Exec(Product);

            return RedirectToPage("Index");
        }
    }
}