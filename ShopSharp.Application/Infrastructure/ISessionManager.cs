using System.Collections.Generic;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(int stockId, int quantity);
        void RemoveProduct(int stockId, int quantity);
        List<CartProduct> GetCart();

        void AddCustomerInformation(CustomerInformation customerInformation);
        CustomerInformation GetCustomerInformation();
    }
}