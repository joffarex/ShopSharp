using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.Cart;
using ShopSharp.Database;

namespace ShopSharp.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, new GetCart(HttpContext.Session, _context).Exec());
        }
    }
}