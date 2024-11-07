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
    public class OrderItemRepository : IOrderItemRepository
    {

        private readonly LamazonDbContext _dbContext;

        public OrderItemRepository(LamazonDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public void Delete(int id)
        {
            OrderItem item = _dbContext
                .OrderItems
                .FirstOrDefault(x => x.Id == id);


            _dbContext .OrderItems.Remove(item);
            _dbContext.SaveChanges();
        }

        public OrderItem Get(int id)
        {
            OrderItem orderItem = _dbContext
                .OrderItems 
                .Include(x => x.Order)
                .Include(y => y.Product)
                .Where(x => x.Id == id)
                .FirstOrDefault();  

            return orderItem;
        }

        public List<OrderItem> GetAll()
        {
            return _dbContext
                .OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .ToList();
        }

        public int Insert(OrderItem item)
        {
            _dbContext.OrderItems.Add(item);    
            
            
            _dbContext.SaveChanges();

            return item.Id; 
        }

        public void Update(OrderItem item)
        {
            _dbContext.OrderItems.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
