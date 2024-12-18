using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.ViewModels.DataManager
{
    public class DashboardViewModel
    {
        public int Month { get; set; } // Mjesec (1-12)
        public string MonthName { get; set; } // Ime mjeseca (npr. "Januar")
        public int TotalOrders { get; set; } // Ukupan broj narudžbi
        public decimal TotalRevenue { get; set; } // Ukupna zarada
    }
}
