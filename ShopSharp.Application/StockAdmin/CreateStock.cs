using System.Threading.Tasks;
using ShopSharp.Application.StockAdmin.Dto;
using ShopSharp.Application.StockAdmin.ViewModels;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.StockAdmin
{
    public class CreateStock
    {
        private readonly ApplicationDbContext _context;

        public CreateStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StockViewModel> ExecAsync(StockDto stockDto)
        {
            var stock = new Stock
            {
                Description = stockDto.Description,
                Quantity = stockDto.Quantity,
                ProductId = stockDto.ProductId
            };

            _context.Stocks.Add(stock);

            await _context.SaveChangesAsync();

            return new StockViewModel
            {
                Id = stock.Id,
                Description = stock.Description,
                Quantity = stock.Quantity
            };
        }
    }
}