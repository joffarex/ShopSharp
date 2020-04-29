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

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Exec();

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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            new AddCustomerInformation(HttpContext.Session).Exec(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}