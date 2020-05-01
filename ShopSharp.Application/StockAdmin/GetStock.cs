using System.Collections.Generic;
using System.Linq;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.StockAdmin
{
    [Service]
    public class GetStock
    {
        private readonly IStockManager _stockManager;

        public GetStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public IEnumerable<ProductViewModel> Exec()
        {
            return _stockManager.GetProductsWithStock(x => new ProductViewModel
            {
                Id = x.Id,
                Description = x.Description,
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