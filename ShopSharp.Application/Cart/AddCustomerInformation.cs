using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.Dto;

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
            var jsonCustomerInformation = JsonConvert.SerializeObject(customerInformationDto);

            _session.SetString("customer-info", jsonCustomerInformation);
        }
    }
}