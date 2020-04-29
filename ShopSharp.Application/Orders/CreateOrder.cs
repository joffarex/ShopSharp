using System;
using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.Orders.Dto;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Orders
{
    public class CreateOrder
    {
        private readonly ApplicationDbContext _context;

        public CreateOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Exec(CreateOrderDto createOrderDto)
        {
            var stocksToUpdate = _context.Stocks.AsEnumerable()
                .Where(
                    x => createOrderDto.Stocks.Any(y => y.StockId == x.Id)
                ).ToList();

            foreach (var stock in stocksToUpdate)
            {
                var orderStock = createOrderDto.Stocks.FirstOrDefault(x => x.StockId == stock.Id);

                if (orderStock != null) stock.Quantity -= orderStock.Quantity;
            }

            var order = new Order
            {
                OrderRef = CreateOrderRef(),
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

            _context.Orders.Add(order);

            return await _context.SaveChangesAsync() > 0;
        }

        public string CreateOrderRef()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new char[12];
            var random = new Random();

            do
            {
                for (var i = 0; i < result.Length; ++i) result[i] = chars[random.Next(chars.Length)];
                // if somehow OrderRef already exists
            } while (_context.Orders.Any(x => x.OrderRef == new string(result)));

            return new string(result);
        }
    }
}