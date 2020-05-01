using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.Orders.Dto;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Orders
{
    public class CreateOrder
    {
        private readonly IOrderManager _orderManager;
        private readonly IStockManager _stockManager;

        public CreateOrder(IStockManager stockManager, IOrderManager orderManager)
        {
            _stockManager = stockManager;
            _orderManager = orderManager;
        }

        public async Task<bool> ExecAsync(CreateOrderDto createOrderDto)
        {
            var order = new Order
            {
                OrderRef = _orderManager.CreateOrderRef(),
                StripeRef = createOrderDto.StripeRef,
                FirstName = createOrderDto.FirstName,
                LastName = createOrderDto.LastName,
                Email = createOrderDto.Email,
                PhoneNumber = createOrderDto.PhoneNumber,
                Address = createOrderDto.Address,
                City = createOrderDto.City,
                PostCode = createOrderDto.PostCode,

                OrderStocks = createOrderDto.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Quantity = x.Quantity
                }).ToList()
            };

            var success = await _orderManager.CreateOrder(order) > 0;

            if (!success) return false;

            await _stockManager.RemoveStockFromHold(createOrderDto.SessionId);

            return true;
        }
    }
}