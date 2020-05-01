using System.Collections.Generic;
using ShopSharp.Application.ProductsAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.ProductsAdmin
{
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductsViewModel> Exec()
        {
            return _productManager.GetProducts(x => new ProductsViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value.GetFormattedValue()
            });
        }
    }
}