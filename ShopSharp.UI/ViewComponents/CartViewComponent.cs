using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.Cart;

namespace ShopSharp.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly GetCart _getCart;

        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            if (view == "Small")
            {
                var totalValue = _getCart.Exec().Sum(x => x.RealValue * x.Quantity);
                return View(view, $"${totalValue}");
            }

            return View(view, _getCart.Exec());
        }
    }
}