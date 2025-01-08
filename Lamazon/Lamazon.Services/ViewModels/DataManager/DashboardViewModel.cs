using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.ViewModels.DataManager
{
    public class DashboardViewModel
    {
        public int Month { get; set; } 
        public string MonthName { get; set; } 
        public int TotalOrders { get; set; } 
        public decimal TotalRevenue { get; set; } 
    }
}
