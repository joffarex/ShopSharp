using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.Orders.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.Orders
{
    public class GetOrder
    {
        private readonly ApplicationDbContext _context;

        public GetOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderViewModel Exec(string orderRef)
        {
            return _context.Orders.Where(x => x.OrderRef == orderRef)
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .Select(x => new OrderViewModel
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
                }).FirstOrDefault();
        }
    }
}