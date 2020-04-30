using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.Products.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.Products
{
    public class GetProduct
    {
        private readonly ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> ExecAsync(string name)
        {
            var stocksOnHold = _context.StocksOnHold.AsEnumerable()
                .Where(x => x.ExpiryDate < DateTime.Now).ToList();

            if (stocksOnHold.Count > 0)
            {
                var stockToReturn = _context.Stocks.AsEnumerable().Where(
                    x => stocksOnHold.Any(y => y.StockId == x.Id)
                ).ToList();

                foreach (var stock in stockToReturn)
                {
                    var stockOnHold = stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id);
                    if (stockOnHold != null) stock.Quantity += stockOnHold.Quantity;

                    _context.StocksOnHold.RemoveRange(stocksOnHold);

                    await _context.SaveChangesAsync();
                }
            }

            return _context.Products.Include(x => x.Stocks).Where(x => x.Name == name).Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = $"${x.Value:N2}", // 69420.60 => $ 69,420.60
                StockCount = x.Stocks.Sum(y => y.Quantity),
                Stocks = x.Stocks.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Quantity = y.Quantity
                })
            }).FirstOrDefault();
        }
    }
}