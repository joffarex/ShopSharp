using System.Threading.Tasks;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.StockAdmin
{
    public class DeleteStock
    {
        private readonly IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<bool> ExecAsync(int id)
        {
            return await _stockManager.DeleteStock(id) > 0;
        }
    }
}