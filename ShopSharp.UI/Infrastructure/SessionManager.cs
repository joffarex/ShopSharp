using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private const string CartSessionKey = "cart";
        private const string CustomerInformationSessionKey = "customer-info";
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public string GetId()
        {
            return _session.Id;
        }

        public void AddProduct(CartProduct cartProduct)
        {
            var cartList = new List<CartProduct>();
            var jsonCardProduct = _session.GetString(CartSessionKey);

            if (!string.IsNullOrEmpty(jsonCardProduct))
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCardProduct);

            // If product is already in cart list, increase quantity
            if (cartList.Any(x => x.StockId == cartProduct.StockId))
                cartList.Find(x => x.StockId == cartProduct.StockId).Quantity += cartProduct.Quantity;
            else
                cartList.Add(cartProduct);

            jsonCardProduct = JsonConvert.SerializeObject(cartList);

            _session.SetString(CartSessionKey, jsonCardProduct);
        }

        public void RemoveProduct(int stockId, int quantity)
        {
            List<CartProduct> cart;
            var jsonCartProduct = _session.GetString(CartSessionKey);

            if (string.IsNullOrEmpty(jsonCartProduct)) return;

            cart = JsonConvert.DeserializeObject<List<CartProduct>>(jsonCartProduct);

            if (!cart.Any(x => x.StockId == stockId)) return;

            var product = cart.First(x => x.StockId == stockId);

            product.Quantity -= quantity;

            if (product.Quantity <= 0) cart.Remove(product);

            jsonCartProduct = JsonConvert.SerializeObject(cart);

            _session.SetString(CartSessionKey, jsonCartProduct);
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var jsonCardProduct = _session.GetString(CartSessionKey);

            if (string.IsNullOrEmpty(jsonCardProduct)) return new List<TResult>();

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(jsonCardProduct);

            return cartList.Select(selector);
        }

        public void ClearCart()
        {
            _session.Remove(CartSessionKey);
        }

        public void AddCustomerInformation(CustomerInformation customerInformation)
        {
            var jsonCustomerInformation = JsonConvert.SerializeObject(customerInformation);

            _session.SetString(CustomerInformationSessionKey, jsonCustomerInformation);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var jsonCustomerInformation = _session.GetString(CustomerInformationSessionKey);

            if (string.IsNullOrEmpty(jsonCustomerInformation)) return null;

            return JsonConvert.DeserializeObject<CustomerInformation>(jsonCustomerInformation);
        }
    }
}