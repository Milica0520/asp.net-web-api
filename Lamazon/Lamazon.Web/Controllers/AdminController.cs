using Lamazon.AdminAPI;
using Lamazon.AdminAPI.Domein;
using Lamazon.AdminAPI.DTOs;
using Lamazon.Services.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Lamazon.Web.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        
        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7196");

        }
       
        public async Task<IActionResult> AllUsers()
        {
            var response = await _httpClient.GetAsync("/api/users/allUsers");
            
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                return View(users); 
            }
            return View("Error");
        }
    }


}


