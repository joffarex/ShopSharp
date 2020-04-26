using System.Threading.Tasks;
using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private readonly ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Exec(ProductViewModel viewModel)
        {
            _context.Products.Add(new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Value = decimal.Parse(viewModel.Value)
            });

            await _context.SaveChangesAsync();
        }
    }
}