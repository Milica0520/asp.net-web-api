using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    public class MenagerController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
