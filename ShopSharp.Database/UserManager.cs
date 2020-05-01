using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Database
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateManagerUser(string username, string password)
        {
            var managerUser = new IdentityUser
            {
                UserName = username
            };

            await _userManager.CreateAsync(managerUser, password);

            var managerClaim = new Claim("Role", "Manager");
            await _userManager.AddClaimAsync(managerUser, managerClaim);

            return true;
        }
    }
}