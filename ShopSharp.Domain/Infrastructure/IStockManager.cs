using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSharp.Domain.Models;

namespace ShopSharp.Domain.Infrastructure
{
    public interface IStockManager
    {
        Stock GetStockWithProduct(int stockId);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
        Task<int> CreateStock(Stock stock);
        Task<int> DeleteStock(int id);
        Task<int> UpdateStockRange(IEnumerable<Stock> stockList);
        bool EnoughStock(int stockId, int quantity);
        Task<int> PutStockOnHold(int stockId, int quantity, string sessionId);
        Task RetrieveExpiresStockOnHold();
        Task<int> RemoveStockFromHold(string sessionId);
        Task<int> RemoveStockFromHold(int stockId, int quantity, string sessionId);
    }
}