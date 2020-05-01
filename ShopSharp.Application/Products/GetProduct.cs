using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.Products.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Products
{
    public class GetProduct
    {
        private readonly IProductManager _productManager;
        private readonly IStockManager _stockManager;

        public GetProduct(IStockManager stockManager, IProductManager productManager)
        {
            _stockManager = stockManager;
            _productManager = productManager;
        }

        public async Task<ProductViewModel> ExecAsync(string name)
        {
            await _stockManager.RetrieveExpiresStockOnHold();

            return _productManager.GetProductByName(name, x => new ProductViewModel
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