using Lamazon.AdminAPI.Domein;
using Lamazon.AdminAPI.DTOs;
using Lamazon.AdminAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {


        private readonly IUserService _userService;   

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("allUsers")]
        public IActionResult GetAllUsers()
        {
            List<UserDto> users = _userService.GetUsers();

            return Ok(users); 

        }

    }


}
