using System;
using System.Collections.Generic;
using System.Linq;
using EasyFoodOrder.Common.DataAccess.Database;
using EasyFoodOrder.Common.DataAccess.Models.Order;

namespace EasyFoodOrder.Services.Order
{
    public class OrderService: IOrderService
    {
        private readonly OrderDbContext _dbContext;

        public OrderService(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateOrder(IEnumerable<OrderItemModel> orderItems)
        {
            var newOrder = new OrderModel
            {
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            foreach (var orderItem in orderItems)
            {
                orderItem.OrderId = newOrder.Id;
                orderItem.CreatedAt = DateTime.UtcNow;
                orderItem.IsDeleted = false;
                _dbContext.OrderItems.Add(orderItem);
            }

            _dbContext.SaveChanges();
            return newOrder.Id;
        }
    }
}