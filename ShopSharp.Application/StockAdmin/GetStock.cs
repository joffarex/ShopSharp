using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.StockAdmin
{
    public class GetStock
    {
        private readonly ApplicationDbContext _context;

        public GetStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Exec()
        {
            var stock = _context.Products.Include(x => x.Stocks)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Stocks = x.Stocks.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Quantity = y.Quantity
                    })
                }).ToList();

            return stock;
        }
    }
}