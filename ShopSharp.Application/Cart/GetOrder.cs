using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Cart
{
    public class GetOrder
    {
        private readonly ISessionManager _sessionManager;

        public GetOrder(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public OrderViewModel Exec()
        {
            var listOfProducts = _sessionManager
                .GetCart(x => new OrderProductDto
                {
                    ProductId = x.ProductId,
                    StockId = x.StockId,
                    Value = (int) (x.Value * 100),
                    Quantity = x.Quantity
                });

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