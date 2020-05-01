using System.Threading.Tasks;
using ShopSharp.Application.ProductsAdmin.Dto;
using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;

        public UpdateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<ProductViewModel> ExecAsync(int id, ProductDto productDto)
        {
            var product = _productManager.GetProductById(id, x => x);

            if (product == null) return null;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Value = decimal.Parse(productDto.Value);

            var success = await _productManager.UpdateProduct(product) > 0;

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