using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implemetations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;  
        
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository; 
            _productRepository = productRepository;


        }

        public OrderItemService() { }
        public void CreateOrderItem(int productId, int orderId)
        {
            Product product = _productRepository.Get(productId);

            OrderItem orderItem = new OrderItem()
            {
                ProductId = productId,
                OrderId = orderId  ,
                Price =product.Price,
                Quantity = 1,

            };

            _orderItemRepository.Insert(orderItem);
        }
    }
}
