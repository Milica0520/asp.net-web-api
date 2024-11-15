using Lamazon.Services.ViewModels.Product;
using Lamazon.Services.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.ViewModels.OrderItem
{
    public class OrderItemVM
    {

        public int Id { get; set; }
        public int OrderId { get; set; }

        public ProductViewModel Product { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
