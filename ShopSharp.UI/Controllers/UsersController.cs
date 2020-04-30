using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.UsersAdmin;
using ShopSharp.Application.UsersAdmin.Dto;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly CreateUser _createUser;

        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }

        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            await _createUser.Exec(userDto);

            return Ok();
        }
    }
}