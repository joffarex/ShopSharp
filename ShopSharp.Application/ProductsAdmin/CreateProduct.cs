using System.Threading.Tasks;
using ShopSharp.Application.ProductsAdmin.Dto;
using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private readonly IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<ProductViewModel> ExecAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Value = decimal.Parse(productDto.Value)
            };

            var success = await _productManager.CreateProduct(product) > 0;

            if (!success) return null;

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value.GetFormattedValue()
            };
        }
    }
}