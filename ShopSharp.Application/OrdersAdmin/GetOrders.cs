using System.Collections.Generic;
using System.Linq;
using ShopSharp.Application.OrdersAdmin.ViewModels;
using ShopSharp.Database;
using ShopSharp.Domain.Enums;

namespace ShopSharp.Application.OrdersAdmin
{
    public class GetOrders
    {
        private readonly ApplicationDbContext _context;

        public GetOrders(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrdersViewModel> Exec(int status)
        {
            return _context.Orders.Where(x => x.Status == (OrderStatus) status)
                .Select(x => new OrdersViewModel
                {
                    Id = x.Id,
                    OrderRef = x.OrderRef,
                    Email = x.Email
                }).ToList();
        }
    }
}