using Lamazon.AdminAPI;
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
            _httpClient.BaseAddress = new Uri("https://localhost:7196/");
        }
        
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("weatherforecast");  

            if (response.IsSuccessStatusCode)
            {
                var weatherData = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();  
                return View(weatherData);  
            }

            return View("Error");  
        }


    }

}
