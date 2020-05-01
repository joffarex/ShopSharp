using System.Collections.Generic;
using System.Linq;
using ShopSharp.Application.Products.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Products
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Exec()
        {
            return _productManager.GetProducts(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.GetFormattedValue(),
                StockCount = x.Stocks.Sum(y => y.Quantity),
                Stocks = x.Stocks.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Quantity = y.Quantity
                })
            });
        }
    }
}