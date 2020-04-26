using System.Collections.Generic;
using System.Linq;
using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.ProductsAdmin
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
            return _context.Products.ToList().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = $"$ {x.Value:N2}" // 69420.60 => $ 69,420.60
            });
        }
    }
}