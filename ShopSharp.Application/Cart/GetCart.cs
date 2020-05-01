using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Application.Infrastructure;
using ShopSharp.Database;

namespace ShopSharp.Application.Cart
{
    public class GetCart
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public IEnumerable<CartViewModel> Exec()
        {
            var cart = _sessionManager.GetCart();

            if (cart == null) return new List<CartViewModel>();

            return cart.Select(product => _context.Stocks.Include(y => y.Product)
                .Where(x => x.Id == product.StockId)
                .Select(x => new CartViewModel
                {
                    Name = x.Product.Name,
                    Value = $"${x.Product.Value:N2}",
                    RealValue = x.Product.Value,
                    StockId = product.StockId,
                    Quantity = product.Quantity
                }).FirstOrDefault()
            ).ToList();
        }
    }
}