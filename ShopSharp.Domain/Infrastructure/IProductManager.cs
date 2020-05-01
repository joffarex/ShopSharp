using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSharp.Domain.Models;

namespace ShopSharp.Domain.Infrastructure
{
    public interface IProductManager
    {
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(int id);
        Task<int> UpdateProduct(Product product);
        IEnumerable<TResult> GetProducts<TResult>(Func<Product, TResult> selector);
    }
}