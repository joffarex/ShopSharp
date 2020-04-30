using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShopSharp.Application.UsersAdmin.Dto;

namespace ShopSharp.Application.UsersAdmin
{
    public class CreateUser
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExecAsync(UserDto userDto)
        {
            var managerUser = new IdentityUser
            {
                UserName = userDto.Username
            };

            await _userManager.CreateAsync(managerUser, "password");

            var managerClaim = new Claim("Role", "Manager");
            await _userManager.AddClaimAsync(managerUser, managerClaim);

            return true;
        }
    }
}