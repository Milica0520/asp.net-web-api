using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lamazon.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Register()
        {
            RegisterUserViewModel registerUserViewModel = new RegisterUserViewModel();  
            return View(registerUserViewModel);
        }


        [HttpPost]  

        public IActionResult Register([FromForm]RegisterUserViewModel model) { 
            _userService.RegisterUser(model);
            return View("SuccessRegistration"); 
        }

        [HttpGet]
        public IActionResult LogIn() 
        {
            LogInUserViewModel logInUserViewModel = new LogInUserViewModel();
            return View(logInUserViewModel);
        }

        [HttpPost]
        public IActionResult LogIn([FromForm] LogInUserViewModel model)
        {
            try
            {
                UserViewModel user = _userService.LogInUser(model);
                if(user == null)
                {
                    return BadRequest();
                }
                List<Claim> userClames = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name , user.FullName),
                    new Claim(ClaimTypes.Role , user.UserRoleKey)
                };

                ClaimsIdentity claimsIdentity
                    = new ClaimsIdentity(userClames, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex) 
            {
                //
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdString);

            UserInfoVM userProfile = _userService.GetUserById(userId);

            return View(userProfile);
        }
    }
}
