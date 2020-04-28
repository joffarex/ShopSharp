using System.ComponentModel.DataAnnotations;

namespace ShopSharp.Application.Cart.Dto
{
    public class CustomerInformationDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required] public string Address { get; set; }
        [Required] public string City { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
    }
}