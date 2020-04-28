using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopSharp.Application.Cart.ViewModels;

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

            return JsonConvert.DeserializeObject<CustomerInformationViewModel>(jsonCustomerInformation);
        }
    }
}