using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.ViewModels;
using ShopSharp.Domain.Models;

namespace ShopSharp.Application.Cart
{
    public class GetCustomerInformation
    {
        private readonly ISession _session;

        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public CustomerInformationViewModel Exec()
        {
            var jsonCustomerInformation = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(jsonCustomerInformation)) return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(jsonCustomerInformation);

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