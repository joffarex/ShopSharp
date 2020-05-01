using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var managerUser = new IdentityUser
            {
                UserName = userDto.Username
            };

            await _userManager.CreateAsync(managerUser, userDto.Password);

            var managerClaim = new Claim("Role", "Manager");
            await _userManager.AddClaimAsync(managerUser, managerClaim);

            return Ok();
        }

        public class UserDto
        {
            [Required] public string Username { get; set; }
            [Required] public string Password { get; set; }
        }
    }
}