using System.Threading.Tasks;
using ShopSharp.Application.StockAdmin.Dto;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.StockAdmin
{
    public class CreateStock
    {
        private readonly IStockManager _stockManager;

        public CreateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<StockViewModel> ExecAsync(StockDto stockDto)
        {
            var stock = new Stock
            {
                Description = stockDto.Description,
                Quantity = stockDto.Quantity,
                ProductId = stockDto.ProductId
            };

            var success = await _stockManager.CreateStock(stock) > 0;

            if (!success) return null;

            return new StockViewModel
            {
                Id = stock.Id,
                Description = stock.Description,
                Quantity = stock.Quantity
            };
        }
    }
}