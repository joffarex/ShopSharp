using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Orders;
using ShopSharp.Application.Orders.Dto;
using ShopSharp.Application.Orders.ViewModels;
using ShopSharp.Database;
using Stripe;
using GetOrder = ShopSharp.Application.Cart.GetOrder;

namespace ShopSharp.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly CreateOrder _createOrder;

        public PaymentModel(IConfiguration configuration, ApplicationDbContext context, CreateOrder createOrder)
        {
            _context = context;
            _createOrder = createOrder;
            PublishableKey = configuration["Stripe:PublishableKey"];
        }

        public string PublishableKey { get; }


        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Exec();

            if (information == null) return RedirectToPage("/Checkout/CustomerInformation");

            return Page();
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken)
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

            var sessionId = HttpContext.Session.Id;

            await _createOrder.ExecAsync(new CreateOrderDto
            {
                SessionId = sessionId,
                StripeRef = charge.Id,
                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                Address = cartOrder.CustomerInformation.Address,
                City = cartOrder.CustomerInformation.City,
                PostCode = cartOrder.CustomerInformation.PostCode,

                Stocks = cartOrder.Products.Select(x => new StockViewModel
                {
                    StockId = x.StockId,
                    Quantity = x.Quantity
                }).ToList()
            });

            return RedirectToPage("/Index");
        }
    }
}