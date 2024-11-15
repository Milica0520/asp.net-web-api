using Lamazon.Services.ViewModels.OrderItem;
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
        public OrderVM()
        {
            Items = new List<OrderItemVM>();
        }
        public int ID { get; set; }

        public string OrderNum { get; set;}

        public DateTime CreatedDate { get; set; }   

        public int UserId { get; set; } 

        public UserViewModel User { get; set; }

        public List<OrderItemVM> Items { get; set; }

        public decimal TotalPrice { get; set; }

        // Shipping Details
        public string? ShippingUserFullName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
