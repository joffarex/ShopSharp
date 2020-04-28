using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;

namespace ShopSharp.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty] public CustomerInformationDto CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Exec();

            if (information == null) return Page();

            return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            new AddCustomerInformation(HttpContext.Session).Exec(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}