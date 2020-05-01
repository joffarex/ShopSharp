using System.Threading.Tasks;
using ShopSharp.Application.UsersAdmin.Dto;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.UsersAdmin
{
    public class CreateUser
    {
        private readonly IUserManager _userManager;

        public CreateUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExecAsync(UserDto userDto)
        {
            return await _userManager.CreateManagerUser(userDto.Username, userDto.Password);
        }
    }
}