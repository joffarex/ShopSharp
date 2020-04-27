using System.Linq;
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

        public ProductViewModel Exec(string name)
        {
            return _context.Products.Include(x => x.Stocks).Where(x => x.Name == name).Select(x => new ProductViewModel
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
            }).FirstOrDefault();
        }
    }
}