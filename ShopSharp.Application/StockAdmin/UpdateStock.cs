using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.StockAdmin.Dto;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.StockAdmin
{
    public class UpdateStock
    {
        private readonly ApplicationDbContext _context;

        public UpdateStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateStockViewModel> Exec(UpdateStockDto updateStockDto)
        {
            var stocks = updateStockDto.Stocks.Select(stock => new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Quantity = stock.Quantity,
                    ProductId = stock.ProductId
                })
                .ToList();

            _context.Stocks.UpdateRange(stocks);

            await _context.SaveChangesAsync();

            return new UpdateStockViewModel
            {
                Stocks = updateStockDto.Stocks
            };
        }
    }
}