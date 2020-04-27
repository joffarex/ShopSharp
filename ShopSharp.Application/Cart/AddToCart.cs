using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class AddToCart
    {
        private readonly ISession _session;

        public AddToCart(ISession session)
        {
            _session = session;
        }

        public void Exec(CartDto cartDto)
        {
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
        }
    }
}