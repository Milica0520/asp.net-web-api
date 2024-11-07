using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IOrderItemService
    {
        void CreateOrderItem(int productId, int orderId);
    }
}
