using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.ProductsAdmin
{
    public class GetProduct
    {
        private readonly IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public ProductViewModel Exec(int id)
        {
            return _productManager.GetProductById(id, x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.GetFormattedValue()
            });
        }
    }
}