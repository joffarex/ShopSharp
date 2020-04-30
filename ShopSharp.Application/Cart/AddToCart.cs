using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class AddToCart
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;

        public AddToCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public async Task<bool> ExecAsync(CartDto cartDto)
        {
            var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _session.Id).ToList();
            var stockToHold = _context.Stocks.Where(x => x.Id == cartDto.StockId).FirstOrDefault();

            if (stockToHold == null) return false;

            if (stockToHold.Quantity < cartDto.Quantity) return false;

            _context.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                SessionId = _session.Id,
                Quantity = cartDto.Quantity,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            });

            stockToHold.Quantity -= cartDto.Quantity;

            foreach (var stock in stockOnHold) stock.ExpiryDate = DateTime.Now.AddMinutes(20);

            await _context.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var jsonCardProduct = _session.GetString("cart");

            if (!string.IsNullOrEmpty(jsonCardProduct))
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCardProduct);

            // If product is already in cart list, increase quantity
            if (cartList.Any(x => x.StockId == cartDto.StockId))
                cartList.Find(x => x.StockId == cartDto.StockId).Quantity += cartDto.Quantity;
            else
                cartList.Add(new CartProduct
                {
                    StockId = cartDto.StockId,
                    Quantity = cartDto.Quantity
                });

            jsonCardProduct = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", jsonCardProduct);

            return true;
        }
    }
}