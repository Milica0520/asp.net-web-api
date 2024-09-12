using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersHomework.Base;

namespace UsersHomework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController() { }


        [HttpGet("getAllUsers")]
        public ActionResult<string[]> GetAllUsers() {

            var allUsers = StaticDB.Users;
            return allUsers;
        }


        [HttpGet("getUserById/{id}")]

        public ActionResult <string> GetUserById(int id) {

            var user = StaticDB.Users.ElementAt(id);

            return user;
        
        
        }

    }
}
