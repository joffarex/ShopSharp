using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShopSharp.Application.Cart;
using ShopSharp.Database;
using Stripe;

namespace ShopSharp.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PaymentModel(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            PublishableKey = configuration["Stripe:PublishableKey"];
        }

        public string PublishableKey { get; }


        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Exec();

            if (information == null) return RedirectToPage("/Checkout/CustomerInformation");

            return Page();
        }

        public IActionResult OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = new GetOrder(HttpContext.Session, _context).Exec();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = cartOrder.GetTotalCharge(),
                Description = "Shop Purchase",
                Currency = "usd",
                Customer = customer.Id
            });

            return RedirectToPage("/Index");
        }
    }
}