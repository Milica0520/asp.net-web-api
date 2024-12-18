﻿using Lamazon.Services.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderVM> GttAllOrders();

        List<UserOrderVM> GetOrdersByUserId(int userId);
        OrderVM GetOrderById(int id);

        void CreateOrder(CreateOrderViemModel model);

        OrderVM GetActiveOrder(int userId);

        OrderVM SubmitOrder(OrderVM order);

        void SetInactiveOrder(int id);
    }
}
