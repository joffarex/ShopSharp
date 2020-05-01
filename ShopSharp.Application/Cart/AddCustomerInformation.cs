using ShopSharp.Application.Cart.Dto;
using ShopSharp.Application.Infrastructure;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class AddCustomerInformation
    {
        private readonly ISessionManager _sessionManager;

        public AddCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Exec(CustomerInformationDto customerInformationDto)
        {
            _sessionManager.AddCustomerInformation(new CustomerInformation
            {
                FirstName = customerInformationDto.FirstName,
                LastName = customerInformationDto.LastName,
                Email = customerInformationDto.Email,
                PhoneNumber = customerInformationDto.PhoneNumber,
                Address = customerInformationDto.Address,
                City = customerInformationDto.City,
                PostCode = customerInformationDto.PostCode
            });
        }
    }
}