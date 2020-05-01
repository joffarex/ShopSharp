using System.Linq;
using ShopSharp.Application.Orders.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Orders
{
    public class GetOrder
    {
        private readonly IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public OrderViewModel Exec(string orderRef)
        {
            return _orderManager.GetOrderByReference(orderRef, x => new OrderViewModel
            {
                OrderRef = x.OrderRef,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                City = x.City,
                PostCode = x.PostCode,
                Products = x.OrderStocks.Select(y => new ProductViewModel
                {
                    Name = y.Stock.Product.Name,
                    Description = y.Stock.Product.Description,
                    Value = $"${y.Stock.Product.Value:N2}",
                    Quantity = y.Quantity,
                    StockDescription = y.Stock.Description
                }),
                TotalValue = $"${x.OrderStocks.Sum(y => y.Stock.Product.Value):N2}"
            });
        }
    }
}