using System.Threading.Tasks;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.OrdersAdmin
{
    [Service]
    public class UpdateOrder
    {
        private readonly IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public async Task<bool> ExecAsync(int id)
        {
            return await _orderManager.AdvanceOrder(id) > 0;
        }
    }
}