using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public string GetId()
        {
            return _session.Id;
        }

        public void AddProduct(int stockId, int quantity)
        {
            var cartList = new List<CartProduct>();
            var jsonCardProduct = _session.GetString("cart");

            if (!string.IsNullOrEmpty(jsonCardProduct))
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCardProduct);

            // If product is already in cart list, increase quantity
            if (cartList.Any(x => x.StockId == stockId))
                cartList.Find(x => x.StockId == stockId).Quantity += quantity;
            else
                cartList.Add(new CartProduct
                {
                    StockId = stockId,
                    Quantity = quantity
                });

            jsonCardProduct = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", jsonCardProduct);
        }

        public void RemoveProduct(int stockId, int quantity)
        {
            List<CartProduct> cart;
            var jsonCartProduct = _session.GetString("cart");

            if (string.IsNullOrEmpty(jsonCartProduct)) return;

            cart = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCartProduct);

            if (!cart.Any(x => x.StockId == stockId)) return;

            var product = cart.First(x => x.StockId == stockId);

            product.Quantity -= quantity;

            if (product.Quantity <= 0) cart.Remove(product);

            jsonCartProduct = JsonConvert.SerializeObject(cart);

            _session.SetString("cart", jsonCartProduct);
        }

        public List<CartProduct> GetCart()
        {
            var jsonCardProduct = _session.GetString("cart");

            if (string.IsNullOrEmpty(jsonCardProduct)) return null;

            return JsonConvert.DeserializeObject<List<CartProduct>>(jsonCardProduct);
        }

        public void AddCustomerInformation(CustomerInformation customerInformation)
        {
            var jsonCustomerInformation = JsonConvert.SerializeObject(customerInformation);

            _session.SetString("customer-info", jsonCustomerInformation);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var jsonCustomerInformation = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(jsonCustomerInformation)) return null;

            return JsonConvert.DeserializeObject<CustomerInformation>(jsonCustomerInformation);
        }
    }
}