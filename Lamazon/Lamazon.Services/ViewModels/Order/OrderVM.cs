using Lamazon.Services.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.ViewModels.Order
{
    public class OrderVM
    {
        public int ID { get; set; }

        public string OrderNum { get; set;}

        public DateTime CreatedDate { get; set; }   

        public int UserId { get; set; } 

        public UserViewModel User { get; set; }
    }
}
