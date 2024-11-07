using Lamazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order Get(int id);

        int Insert(Order order);
        void Update(Order order);
        void Delete(int id);
        Order GetActiveOrder(int userId);
    }
}
