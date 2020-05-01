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
    public class RemoveFromCart
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;

        public RemoveFromCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }


        public async Task<bool> ExecAsync(CartDto cartDto)
        {
            List<CartProduct> cartList;
            var jsonCardProduct = _session.GetString("cart");

            if (string.IsNullOrEmpty(jsonCardProduct)) return true;

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCardProduct);

            if (!cartList.Any(x => x.StockId == cartDto.StockId))
                return true;

            cartList.Find(x => x.StockId == cartDto.StockId).Quantity -= cartDto.Quantity;

            var stockOnHold = _context.StocksOnHold.FirstOrDefault(
                x => x.StockId == cartDto.StockId && x.SessionId == _session.Id
            );

            if (stockOnHold == null) return false;

            var stock = _context.Stocks.FirstOrDefault(x => x.Id == cartDto.StockId);

            if (stock == null) return false;

            if (cartDto.All)
            {
                stock.Quantity += stockOnHold.Quantity;
                stockOnHold.Quantity = 0;

                var stockToRemove = cartList.Single(x => x.StockId == cartDto.StockId);
                cartList.Remove(stockToRemove);
            }
            else
            {
                stock.Quantity += cartDto.Quantity;
                stockOnHold.Quantity -= cartDto.Quantity;
            }

            if (stockOnHold.Quantity <= 0) _context.Remove(stockOnHold);

            await _context.SaveChangesAsync();

            jsonCardProduct = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", jsonCardProduct);

            return true;
        }
    }
}