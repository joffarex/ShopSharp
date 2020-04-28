using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.Dto;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class AddCustomerInformation
    {
        private readonly ISession _session;

        public AddCustomerInformation(ISession session)
        {
            _session = session;
        }

        public void Exec(CustomerInformationDto customerInformationDto)
        {
            var customerInformation = new CustomerInformation
            {
                FirstName = customerInformationDto.FirstName,
                LastName = customerInformationDto.LastName,
                Email = customerInformationDto.Email,
                PhoneNumber = customerInformationDto.PhoneNumber,
                Address = customerInformationDto.Address,
                City = customerInformationDto.City,
                PostCode = customerInformationDto.PostCode
            };

            var jsonCustomerInformation = JsonConvert.SerializeObject(customerInformation);

            _session.SetString("customer-info", jsonCustomerInformation);
        }
    }
}