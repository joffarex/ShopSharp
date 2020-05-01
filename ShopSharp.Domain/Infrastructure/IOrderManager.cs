using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSharp.Domain.Enums;
using ShopSharp.Domain.Models;

namespace ShopSharp.Domain.Infrastructure
{
    public interface IOrderManager
    {
        string CreateOrderRef();
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        TResult GetOrderByReference<TResult>(string orderRef, Func<Order, TResult> selector);
        Task<int> CreateOrder(Order order);
        Task<int> AdvanceOrder(int id);
    }
}