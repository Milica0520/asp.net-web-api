using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;

namespace Lamazon.Web.Controllers
{
    public class OrderController : Controller
    {
      
        private readonly IOrderService _orderService;
        

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            
        }


        public IActionResult UserOrders() 
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdString);

            List<UserOrderVM> userOrders = _orderService.GetOrdersByUserId(userId);

            return View(userOrders);
        }

        [HttpGet]
        [Authorize]

        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
