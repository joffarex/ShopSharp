using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.Products.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.Products
{
    public class GetProducts
    {
        private readonly ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Exec()
        {
            return _context.Products.Include(x => x.Stocks)
                .ToList()
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value = $"$ {x.Value:N2}", // 69420.60 => $ 69,420.60
                    Stocks = x.Stocks.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        InStock = y.Quantity > 0
                    })
                });
        }
    }
}