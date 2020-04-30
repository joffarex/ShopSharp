using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.OrdersAdmin.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.OrdersAdmin
{
    public class GetOrder
    {
        private readonly ApplicationDbContext _context;

        public GetOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderViewModel Exec(int id)
        {
            return _context.Orders.Where(x => x.Id == id)
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
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
                        Quantity = y.Quantity,
                        StockDescription = y.Stock.Description
                    }),
                    TotalValue = $"$ {x.OrderStocks.Sum(y => y.Stock.Product.Value):N2}"
                }).FirstOrDefault();
        }
    }
}