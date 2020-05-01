using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Infrastructure;
using ShopSharp.Database;

namespace ShopSharp.Application.Cart
{
    public class RemoveFromCart
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionManager _sessionManager;

        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }


        public async Task<bool> ExecAsync(CartDto cartDto)
        {
            var stockOnHold = _context.StocksOnHold.FirstOrDefault(
                x => x.StockId == cartDto.StockId && x.SessionId == _sessionManager.GetId()
            );

            if (stockOnHold == null) return false;

            var stock = _context.Stocks.FirstOrDefault(x => x.Id == cartDto.StockId);

            if (stock == null) return false;

            if (cartDto.All)
            {
                stock.Quantity += stockOnHold.Quantity;
                _sessionManager.RemoveProduct(cartDto.StockId, stockOnHold.Quantity);
                stockOnHold.Quantity = 0;
            }
            else
            {
                stock.Quantity += cartDto.Quantity;
                stockOnHold.Quantity -= cartDto.Quantity;
                _sessionManager.RemoveProduct(cartDto.StockId, cartDto.Quantity);
            }

            if (stockOnHold.Quantity <= 0) _context.Remove(stockOnHold);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}