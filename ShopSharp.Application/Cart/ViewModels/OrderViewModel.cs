using System.Collections.Generic;
using System.Linq;
using ShopSharp.Application.Cart.Dto;

namespace ShopSharp.Application.Cart.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<OrderProductDto> Products { get; set; }
        public CustomerInformationViewModel CustomerInformation { get; set; }

        public int GetTotalCharge()
        {
            return Products.Sum(x => x.Value * x.Quantity);
        }
    }
}