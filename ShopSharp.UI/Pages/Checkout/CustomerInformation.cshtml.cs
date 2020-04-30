using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Database;

namespace ShopSharp.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _environment;

        public CustomerInformationModel(IHostEnvironment environment, ApplicationDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        [BindProperty] public CustomerInformationDto CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            var cart = new GetCart(HttpContext.Session, _context).Exec();

            if (cart.Count() == 0) return RedirectToPage("/Cart");

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