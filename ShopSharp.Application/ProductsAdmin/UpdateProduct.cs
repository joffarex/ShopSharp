using System;
using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Database;

namespace ShopSharp.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private readonly ApplicationDbContext _context;

        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Exec(ProductViewModel viewModel)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == viewModel.Id);

            if (product == null) throw new Exception("Product not found");

            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Value = decimal.Parse(viewModel.Value);

            await _context.SaveChangesAsync();
        }
    }
}