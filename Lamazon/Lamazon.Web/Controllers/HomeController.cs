using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Product;
using Lamazon.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lamazon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel>  allProducts = _productService.GetAllProducts();    
            return View(allProducts);
        }

        [HttpGet]

        public IActionResult ProductDetails(int id) { 
        
            ProductViewModel product = _productService.GetProductById(id);  

            return View(product);
        }


        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
