using System.Collections.Generic;
using System.Linq;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.StockAdmin
{
    public class GetStocks
    {
        private readonly ApplicationDbContext _context;

        public GetStocks(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StockViewModel> Exec(int productId)
        {
            var stock = _context.Stocks.Where(x => x.ProductId == productId)
                .Select(x => new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    ProductId = x.ProductId
                })
                .ToList();

            return stock;
        }
    }
}