using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Lamazon.Services.ViewModels.OrderItem;
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
            _orderRepository = orderRepository;
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
                    },
                    Items = activeOrder.Items
                    .Select(o => new OrderItemVM()
                    {
                        Id = o.Id,
                        OrderId = o.OrderId,
                        Price = o.Price,
                        Qty = o.Quantity,
                        Product = new ViewModels.Product.ProductViewModel()
                        {
                            Name = o.Product.Name,
                            Description = o.Product.Description,
                            ImageUrl = o.Product.ImageUrl,
                            Price = o.Product.Price,
                            Id = o.ProductId
                        }
                    })
                    .ToList()
                };
                activeOrderViewModel.TotalPrice = activeOrderViewModel
        .Items
        .Sum(o => o.Price * o.Qty);
            }
            return activeOrderViewModel;
        }

        public OrderVM GetOrderById(int id)
        {
           throw new NotImplementedException(); 
        }

        public List<UserOrderVM> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetAll()
            .Where(o => o.UserId == userId)
            .Select(o => new UserOrderVM
            {
                ID = o.Id,
                CreatedDate = o.OrderDate,
                OrderNum = o.OrderNumber,
                IsActive = o.IsActive,
                TotalPrice = o.TotalPrice,

            }).ToList();
        }

        public List<OrderVM> GttAllOrders()
        {
            List<OrderVM> orders = 
             _orderRepository.GetAll().Select(o => new OrderVM
            {
                ID = o.Id,
                UserId = o.UserId,
                OrderNum = o.OrderNumber,
                City = o.City,
                Country = o.Country,
                TotalPrice = o.TotalPrice,
                Address = o.Address,
                CreatedDate = DateTime.Now,
                

            }).ToList();
            return orders;  
        }

        public void SetInactiveOrder(int id)
        {
            Order existingActiveOreder = _orderRepository.GetActiveOrder(id);
            existingActiveOreder.IsActive = false;

            _orderRepository.Update(existingActiveOreder);
        }

        public OrderVM SubmitOrder(OrderVM order)
        {
            Order existingActiveOrder = _orderRepository.Get(order.ID);

            if (existingActiveOrder == null)
                throw new Exception($"There is not existing order with provided ID: {order.ID}");

            existingActiveOrder.ShippingUserFullName = order.ShippingUserFullName;
            existingActiveOrder.Address = order.Address;
            existingActiveOrder.City = order.City;
            existingActiveOrder.PostalCode = order.PostalCode;
            existingActiveOrder.Country = order.Country;
            existingActiveOrder.PhoneNumber = order.PhoneNumber;

            // existingActiveOrder.IsActive = false;

            _orderRepository.Update(existingActiveOrder);

            return order;
        }
    }
}
