using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.Cart
{
    public class GetCustomerInformation
    {
        private readonly ISessionManager _sessionManager;

        public GetCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public CustomerInformationViewModel Exec()
        {
            var customerInformation = _sessionManager.GetCustomerInformation();

            if (customerInformation == null) return null;

            return new CustomerInformationViewModel
            {
                FirstName = customerInformation.FirstName,
                LastName = customerInformation.LastName,
                Email = customerInformation.Email,
                PhoneNumber = customerInformation.PhoneNumber,
                Address = customerInformation.Address,
                City = customerInformation.City,
                PostCode = customerInformation.PostCode
            };
        }
    }
}