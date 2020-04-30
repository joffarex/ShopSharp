using System;
using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Database;

namespace ShopSharp.Application.StockAdmin
{
    public class DeleteStock
    {
        private readonly ApplicationDbContext _context;

        public DeleteStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExecAsync(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
            _context.Stocks.Remove(stock ?? throw new Exception("Stock not found"));
            await _context.SaveChangesAsync();

            return true;
        }
    }
}