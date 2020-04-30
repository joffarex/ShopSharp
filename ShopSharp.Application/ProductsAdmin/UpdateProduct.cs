using System;
using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.ProductsAdmin.Dto;
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

        public async Task<ProductViewModel> ExecAsync(int id, ProductDto productDto)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) throw new Exception("Product not found");

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Value = decimal.Parse(productDto.Value);

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