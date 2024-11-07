using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private LamazonDbContext _dBcontext;

        public OrderRepository(LamazonDbContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            Order order = _dBcontext
                .Orders
                .Include (o => o.Id)
                .SingleOrDefault (o => o.Id == id); 

            return order;
        }

        public Order GetActiveOrder(int userId)
        {
            Order order = _dBcontext
                .Orders
                .Include(o => o.User)
                .Where(o => o.IsActive == true)
                .Where(o => o.UserId == userId)
                .FirstOrDefault();

            return order;

        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Order order)
        {
            _dBcontext.Orders.Add(order);  
            _dBcontext.SaveChanges();   

            return order.Id;

        
        }

        public void Update(Order order)
        {
            _dBcontext
                .Orders
                .Update(order);

            _dBcontext.SaveChanges();   
        }
    }
}
