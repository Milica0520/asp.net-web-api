using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Lamazon.Services.ViewModels.OrderItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Serilog;
using Stripe;
using Stripe.Checkout;
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
            string userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdValue);

            OrderVM activeOrderData = _orderService.GetActiveOrder(userId);

            return View(activeOrderData);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Summary()
        {
            string userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdValue);

            OrderVM activeOrderData = _orderService.GetActiveOrder(userId);

            return View(activeOrderData);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Summary([FromForm] OrderVM model)
        {
            try
            {
                _orderService.SubmitOrder(model);

                string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = int.Parse(userIdString);

                OrderVM activeOrder = _orderService.GetActiveOrder(userId);

                


                // TODO ADD PAYMENT METHOD USING STRIPE
                string domain = "https://localhost:7196";

                SessionCreateOptions sessionCreateOptions = new SessionCreateOptions()
                {
                    Mode = "payment",
                    LineItems = new List<SessionLineItemOptions>(),
                    SuccessUrl = $"{domain}/Order/Confirmation?orderNum={model.ID}",
                    CancelUrl = $"{domain}/Order/ShoppingCart",
                };


                foreach (OrderItemVM orderItem in activeOrder.Items)
                {

                    long orderInCent = (long)(orderItem.Price * 100);
                    SessionLineItemOptions productItem = new SessionLineItemOptions()
                    {
                        Quantity = orderItem.Qty,
                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            UnitAmount = orderInCent,
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name = orderItem.Product.Name,
                            },
                        },

                    };

                    sessionCreateOptions.LineItems.Add(productItem);

                }
                SessionService sessionService = new SessionService();


                Session session = sessionService.Create(sessionCreateOptions);


                //Response.Headers.Add("Location", session.Url);

                // return new StatusCodeResult(303);
                return Redirect(session.Url);
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Internal ServerErrror happened {User.FindFirstValue(ClaimTypes.NameIdentifier)}");
                return RedirectToAction("Error", "Home");
            }
           
        }

        [HttpGet]
        [Authorize]
        public IActionResult Confirmation(int orderId)
        {

            OrderVM order = _orderService.GetOrderById(orderId);
            _orderService.SetInactiveOrder(orderId);
                
            ViewData["orderNumber"] = order.OrderNum;
            return View();
        }
    }
}
