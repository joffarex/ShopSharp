using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Orders;
using ShopSharp.Application.Orders.ViewModels;

namespace ShopSharp.UI.Pages
{
    public class OrderModel : PageModel
    {
        private readonly GetOrder _getOrder;

        public OrderModel(GetOrder getOrder)
        {
            _getOrder = getOrder;
        }

        public OrderViewModel Order { get; set; }

        public IActionResult OnGet(string orderRef)
        {
            Order = _getOrder.Exec(orderRef);

            if (Order == null) return RedirectToPage("/Index");

            return Page();
        }
    }
}