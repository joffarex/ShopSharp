using System.Threading.Tasks;
using ShopSharp.Application.ProductsAdmin.Dto;
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

        public async Task<ProductViewModel> ExecAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Value = decimal.Parse(productDto.Value)
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = $"$ {product.Value:N2}"
            };
        }
    }
}