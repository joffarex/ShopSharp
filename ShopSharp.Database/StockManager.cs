using System;
using System.Collections.Generic;
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

        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector)
        {
            return _context.Products
                .Include(x => x.Stocks)
                .Select(selector)
                .ToList();
        }

        public Task<int> CreateStock(Stock stock)
        {
            _context.Stocks.Add(stock);

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteStock(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null) return null;

            _context.Stocks.Remove(stock);

            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateStockRange(IEnumerable<Stock> stockList)
        {
            _context.Stocks.UpdateRange(stockList);

            return _context.SaveChangesAsync();
        }

        public bool EnoughStock(int stockId, int quantity)
        {
            var stockToHold = _context.Stocks.FirstOrDefault(x => x.Id == stockId);

            if (stockToHold == null) return false;

            return stockToHold.Quantity >= quantity;
        }

        public Task<int> PutStockOnHold(int stockId, int quantity, string sessionId)
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

        public Task RetrieveExpiresStockOnHold()
        {
            var stocksOnHold = _context.StocksOnHold.AsEnumerable()
                .Where(x => x.ExpiryDate < DateTime.Now)
                .ToList();

            if (stocksOnHold.Count <= 0) return Task.CompletedTask;

            var stockToReturn = _context.Stocks.AsEnumerable()
                .Where(x => stocksOnHold.Any(y => y.StockId == x.Id))
                .ToList();

            foreach (var stock in stockToReturn)
            {
                var stockOnHold = stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id);
                if (stockOnHold != null) stock.Quantity += stockOnHold.Quantity;

                _context.StocksOnHold.RemoveRange(stocksOnHold);
            }

            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            _context.StocksOnHold.RemoveRange(stockOnHold);

            return _context.SaveChangesAsync();
        }

        public Task<int> RemoveStockFromHold(int stockId, int quantity, string sessionId)
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