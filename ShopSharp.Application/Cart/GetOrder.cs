using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Application.Infrastructure;
using ShopSharp.Database;

namespace ShopSharp.Application.Cart
{
    public class GetOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionManager _sessionManager;

        public GetOrder(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public OrderViewModel Exec()
        {
            var cart = _sessionManager.GetCart();

            var listOfProducts = cart.Select(product => _context.Stocks.Include(x => x.Product)
                .Where(x => x.Id == product.StockId)
                .Select(x => new OrderProductDto
                {
                    ProductId = x.Product.Id,
                    StockId = x.Id,
                    Value = (int) (x.Product.Value * 100),
                    Quantity = product.Quantity
                }).FirstOrDefault()
            ).ToList();

            var customerInformation = _sessionManager.GetCustomerInformation();

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