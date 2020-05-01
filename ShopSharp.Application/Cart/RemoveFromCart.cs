using System.Threading.Tasks;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Cart
{
    public class RemoveFromCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public RemoveFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> ExecAsync(CartDto cartDto)
        {
            if (cartDto.Quantity <= 0) return false;

            await _stockManager.RemoveStockFromHold(cartDto.StockId, cartDto.Quantity, _sessionManager.GetId());

            _sessionManager.RemoveProduct(cartDto.StockId, cartDto.Quantity);

            return true;
        }
    }
}