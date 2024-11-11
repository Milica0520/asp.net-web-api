using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.ViewModels.Order
{
    public class UserOrderVM 
    {
        public int ID { get; set; }

        public string OrderNum { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
        public decimal TotalPrice { get; set; }


    }
}
