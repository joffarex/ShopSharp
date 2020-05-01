using System.Threading.Tasks;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    [Service]
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public AddToCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> ExecAsync(CartDto cartDto)
        {
            if (!_stockManager.EnoughStock(cartDto.StockId, cartDto.Quantity)) return false;

            var success =
                await _stockManager.PutStockOnHold(cartDto.StockId, cartDto.Quantity, _sessionManager.GetId()) > 0;

            if (!success) return false;

            var stock = _stockManager.GetStockWithProduct(cartDto.StockId);

            if (stock == null) return false;

            var cartProduct = new CartProduct
            {
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                StockId = stock.Id,
                Quantity = cartDto.Quantity,
                Value = stock.Product.Value
            };

            _sessionManager.AddProduct(cartProduct);

            return true;
        }
    }
}