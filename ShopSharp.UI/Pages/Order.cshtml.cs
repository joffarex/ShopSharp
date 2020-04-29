using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Orders;
using ShopSharp.Application.Orders.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.UI.Pages
{
    public class OrderModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderViewModel Order { get; set; }

        public IActionResult OnGet(string orderRef)
        {
            Order = new GetOrder(_context).Exec(orderRef);

            if (Order == null) return RedirectToPage("/Index");

            return Page();
        }
    }
}