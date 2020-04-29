using System.Collections.Generic;
using ShopSharp.Application.Orders.ViewModels;

namespace ShopSharp.Application.Orders.Dto
{
    public class CreateOrderDto
    {
        public string StripeRef { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public List<StockViewModel> Stocks { get; set; }
    }
}