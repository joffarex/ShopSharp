using System;
using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Infrastructure;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class AddToCart
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionManager _sessionManager;

        public AddToCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public async Task<bool> ExecAsync(CartDto cartDto)
        {
            var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _sessionManager.GetId()).ToList();
            var stockToHold = _context.Stocks.Where(x => x.Id == cartDto.StockId).FirstOrDefault();

            if (stockToHold == null) return false;

            if (stockToHold.Quantity < cartDto.Quantity) return false;


            if (stockOnHold.Any(x => x.StockId == cartDto.StockId))
                stockOnHold.Find(x => x.StockId == cartDto.StockId).Quantity += cartDto.Quantity;
            else
                _context.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _sessionManager.GetId(),
                    Quantity = cartDto.Quantity,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });

            stockToHold.Quantity -= cartDto.Quantity;

            foreach (var stock in stockOnHold) stock.ExpiryDate = DateTime.Now.AddMinutes(20);

            await _context.SaveChangesAsync();

            _sessionManager.AddProduct(cartDto.StockId, cartDto.Quantity);

            return true;
        }
    }
}