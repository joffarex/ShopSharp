using FluentValidation;
using ShopSharp.Application.Cart.Dto;

namespace ShopSharp.UI.Validation
{
    public class CustomerInformationDtoValidation : AbstractValidator<CustomerInformationDto>
    {
        public CustomerInformationDtoValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(7);
            RuleFor(x => x.Address).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.PostCode).NotNull();
        }
    }
}