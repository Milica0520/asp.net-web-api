using Lamazon.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    public class ProductCategoryController : Controller
    {

        private readonly LamazonDbContext _DbContext;
        public ProductCategoryController  (LamazonDbContext lamazonDbContext)
        {
             _DbContext = lamazonDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var categoties = _DbContext.ProductCategories.ToList();


            return View(categoties);
        }
    }
}
