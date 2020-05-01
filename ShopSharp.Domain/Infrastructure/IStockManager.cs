using System.Threading.Tasks;
using ShopSharp.Domain.Models;

namespace ShopSharp.Domain.Infrastructure
{
    public interface IStockManager
    {
        Stock GetStockWithProduct(int stockId);
        bool EnoughStock(int stockId, int quantity);
        Task PutStockOnHold(int stockId, int quantity, string sessionId);
        Task RemoveStockFromHold(string sessionId);
        Task RemoveStockFromHold(int stockId, int quantity, string sessionId);
    }
}