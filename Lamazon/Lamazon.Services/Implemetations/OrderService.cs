using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implemetations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
        _orderRepository= orderRepository;
          }
        public void CreateOrder(CreateOrderViemModel model)
        {
            Order newOrder = new Order()
            {
                UserId = model.UserId,
                OrderDate = DateTime.UtcNow,
                IsActive = true,
                OrderNumber = $"{DateTime.UtcNow.ToLongTimeString()}_{model.UserId}",
            };

            _orderRepository.Insert(newOrder);  
        }

        public OrderVM GetActiveOrder(int userId)
        {
            Order activeOrder = _orderRepository.GetActiveOrder(userId);

            OrderVM activeOrderViewModel = null;


            if (activeOrder != null)
            {
                activeOrderViewModel = new OrderVM()
                {
                    ID = activeOrder.Id,
                    CreatedDate = activeOrder.OrderDate,
                    OrderNum = activeOrder.OrderNumber,
                    UserId = userId,
                    User = new ViewModels.User.UserViewModel()
                    {
                        FullName = activeOrder.User.FirstName + " " + activeOrder.User.LastName,
                    }
                };
            }
            return activeOrderViewModel;
        }

        public OrderVM GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderVM> GttAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}
