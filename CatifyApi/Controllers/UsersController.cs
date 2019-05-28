using CatifyApi.Database;
using CatifyApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatifyApi.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userService;

        public UsersController(IUserRepository userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Park()
        {
            var user = await _userService.GetAvailableUser();
            if(user != null)
            {
                await _userService.Park(user.Id);
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            await _userService.Add(user);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Unpark(int userId)
        {
            await _userService.Unpark(userId);
            return Ok();
        }
    }
}
