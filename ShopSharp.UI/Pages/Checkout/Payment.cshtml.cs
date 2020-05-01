using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShopSharp.Application.Cart;
using ShopSharp.Application.Orders;
using ShopSharp.Application.Orders.Dto;
using ShopSharp.Application.Orders.ViewModels;
using Stripe;
using GetOrderCart = ShopSharp.Application.Cart.GetOrder;

namespace ShopSharp.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public PaymentModel(IConfiguration configuration)
        {
            PublishableKey = configuration["Stripe:PublishableKey"];
        }

        public string PublishableKey { get; }
        public string TotalValue { get; set; }

        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation,
            [FromServices] GetCart getCart)
        {
            var information = getCustomerInformation.Exec();
            var totalValue = getCart.Exec().Sum(x => x.RealValue * x.Quantity);
            TotalValue = $"${totalValue}";

            if (information == null) return RedirectToPage("/Checkout/CustomerInformation");

            var cart = getCart.Exec();

            if (cart.Count() == 0) return RedirectToPage("/Cart");

            return Page();
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken,
            [FromServices] GetOrderCart getOrder, [FromServices] CreateOrder createOrder)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = getOrder.Exec();

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

            await createOrder.ExecAsync(new CreateOrderDto
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

            HttpContext.Session.Remove("cart");

            return RedirectToPage("/Index");
        }
    }
}