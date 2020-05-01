using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.StockAdmin.Dto;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.StockAdmin
{
    [Service]
    public class UpdateStock
    {
        private readonly IStockManager _stockManager;

        public UpdateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<UpdateStockViewModel> ExecAsync(UpdateStockDto updateStockDto)
        {
            var stocks = updateStockDto.Stocks
                .Select(stock => new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Quantity = stock.Quantity,
                    ProductId = stock.ProductId
                })
                .ToList();

            var success = await _stockManager.UpdateStockRange(stocks) > 0;

            if (!success) return null;

            return new UpdateStockViewModel
            {
                Stocks = updateStockDto.Stocks
            };
        }
    }
}