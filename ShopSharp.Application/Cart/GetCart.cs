using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class GetCart
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;

        public GetCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public IEnumerable<CartViewModel> Exec()
        {
            var jsonCardProduct = _session.GetString("cart");

            if (string.IsNullOrEmpty(jsonCardProduct)) return new List<CartViewModel>();

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCardProduct);

            return cartList.Select(product => _context.Stocks.Include(y => y.Product)
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