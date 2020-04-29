using System.Collections.Generic;

namespace ShopSharp.Application.Orders.ViewModels
{
    public class OrderViewModel
    {
        public string OrderRef { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public string TotalValue { get; set; }
    }
}