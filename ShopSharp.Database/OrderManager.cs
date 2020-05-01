using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Domain.Enums;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Database
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;

        public OrderManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CreateOrderRef()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new char[12];
            var random = new Random();

            do
            {
                for (var i = 0; i < result.Length; ++i) result[i] = chars[random.Next(chars.Length)];
                // if somehow OrderRef already exists
            } while (_context.Orders.Any(x => x.OrderRef == new string(result)));

            return new string(result);
        }

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return _context.Orders
                .Where(x => x.Status == status)
                .Select(selector)
                .ToList();
        }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return GetOrder(order => order.Id == id, selector);
        }

        public TResult GetOrderByReference<TResult>(string orderRef, Func<Order, TResult> selector)
        {
            return GetOrder(order => order.OrderRef == orderRef, selector);
        }

        public Task<int> CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            return _context.SaveChangesAsync();
        }

        public Task<int> AdvanceOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return null;

            order.Status += 1;

            return _context.SaveChangesAsync();
        }

        private TResult GetOrder<TResult>(Func<Order, bool> condition, Func<Order, TResult> selector)
        {
            return _context.Orders
                .Where(x => condition(x))
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .Select(selector)
                .FirstOrDefault();
        }
    }
}