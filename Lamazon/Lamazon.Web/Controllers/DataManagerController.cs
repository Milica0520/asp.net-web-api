using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.DataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    
    public class DataManagerController : Controller
    {
        private readonly IOrderService _orderService;



        public DataManagerController(IOrderService orderService)
        {
            _orderService = orderService;
        }
     

        public IActionResult Dashboard()
        {
            var orders = _orderService.GttAllOrders();
           
            var dashboardData = orders
                .GroupBy(o => o.CreatedDate.Month)
                .Select(g => new DashboardViewModel
                {
                    Month = g.Key,
                    MonthName = new DateTime(2024, g.Key, 1).ToString("MMMM"),
                    TotalOrders = g.Count(),
                    TotalRevenue = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(d => d.Month)
                .ToList();

            return View(dashboardData);
        }

    }
}
