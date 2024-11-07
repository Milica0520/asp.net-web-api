using Lamazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IOrderItemRepository
    {

        List<OrderItem> GetAll();

        OrderItem Get(int id);

        int Insert(OrderItem item); 

        void Update(OrderItem item);

        void Delete(int id);
    }
}
