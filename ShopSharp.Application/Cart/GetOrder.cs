using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Database;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class GetOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;

        public GetOrder(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public OrderViewModel Exec()
        {
            var cart = _session.GetString("cart");

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var listOfProducts = cartList.Select(product => _context.Stocks.Include(x => x.Product)
                .Where(x => x.Id == product.StockId)
                .Select(x => new OrderProductDto
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int) (x.Product.Value * 100),
                    Quantity = product.Quantity
                }).FirstOrDefault()
            ).ToList();

            var jsonCustomerInformation = _session.GetString("customer-info");

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(jsonCustomerInformation);

            return new OrderViewModel
            {
                Products = listOfProducts,
                CustomerInformation = new CustomerInformationViewModel
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber,
                    Address = customerInformation.Address,
                    City = customerInformation.City,
                    PostCode = customerInformation.PostCode
                }
            };
        }
    }
}