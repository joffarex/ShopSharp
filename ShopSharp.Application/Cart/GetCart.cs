using System.Collections.Generic;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Cart
{
    public class GetCart
    {
        private readonly ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IEnumerable<CartViewModel> Exec()
        {
            return _sessionManager
                .GetCart(x => new CartViewModel
                {
                    Name = x.ProductName,
                    Value = x.Value.GetFormattedValue(),
                    RealValue = x.Value,
                    StockId = x.StockId,
                    Quantity = x.Quantity
                });
        }
    }
}