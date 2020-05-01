using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;

namespace ShopSharp.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private readonly IHostEnvironment _environment;

        public CustomerInformationModel(IHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty] public CustomerInformationDto CustomerInformation { get; set; }

        public IActionResult OnGet([FromServices] GetCart getCart,
            [FromServices] GetCustomerInformation getCustomerInformation)
        {
            var cart = getCart.Exec();

            if (cart.Count() == 0) return RedirectToPage("/Cart");

            var information = getCustomerInformation.Exec();

            if (information == null)
            {
                if (_environment.IsDevelopment())
                    CustomerInformation = new CustomerInformationDto
                    {
                        FirstName = "Toko",
                        LastName = "Goshadze",
                        Email = "toko@gmail.com",
                        PhoneNumber = "599744894",
                        Address = "28 Amaghleba street",
                        City = "Tbilisi",
                        PostCode = "0105"
                    };
                return Page();
            }

            return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            if (!ModelState.IsValid) return Page();

            addCustomerInformation.Exec(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}