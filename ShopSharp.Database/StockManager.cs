using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Database
{
    public class StockManager : IStockManager
    {
        private readonly ApplicationDbContext _context;

        public StockManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            var stock = _context.Stocks
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == stockId);

            return stock ?? null;
        }

        public bool EnoughStock(int stockId, int quantity)
        {
            var stockToHold = _context.Stocks.FirstOrDefault(x => x.Id == stockId);

            if (stockToHold == null) return false;

            return stockToHold.Quantity >= quantity;
        }

        public Task PutStockOnHold(int stockId, int quantity, string sessionId)
        {
            var stockToHold = _context.Stocks.FirstOrDefault(x => x.Id == stockId);
            if (stockToHold == null) return null;

            stockToHold.Quantity -= quantity;

            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();
            if (stockOnHold.Any(x => x.StockId == stockId))
                stockOnHold.Find(x => x.StockId == stockId).Quantity += quantity;
            else
                _context.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Quantity = quantity,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });

            foreach (var stock in stockOnHold) stock.ExpiryDate = DateTime.Now.AddMinutes(20);

            return _context.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            _context.StocksOnHold.RemoveRange(stockOnHold);

            return _context.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int quantity, string sessionId)
        {
            var stockOnHold = _context.StocksOnHold
                .FirstOrDefault(x => x.StockId == stockId && x.SessionId == sessionId);
            if (stockOnHold == null) return null;

            var stock = _context.Stocks.FirstOrDefault(x => x.Id == stockId);
            if (stock == null) return null;

            stockOnHold.Quantity -= quantity;
            stock.Quantity += quantity;

            if (stockOnHold.Quantity <= 0) _context.Remove(stockOnHold);

            return _context.SaveChangesAsync();
        }
    }
}