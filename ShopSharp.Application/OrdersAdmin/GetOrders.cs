using System.Collections.Generic;
using ShopSharp.Application.OrdersAdmin.ViewModels;
using ShopSharp.Domain.Enums;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.OrdersAdmin
{
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IEnumerable<OrdersViewModel> Exec(int status)
        {
            return _orderManager.GetOrdersByStatus(
                (OrderStatus) status,
                x => new OrdersViewModel
                {
                    Id = x.Id,
                    OrderRef = x.OrderRef,
                    Email = x.Email
                }
            );
        }
    }
}