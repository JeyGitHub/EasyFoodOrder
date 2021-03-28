using System.Collections.Generic;
using EasyFoodOrder.Common.DataAccess.Models.Order;

namespace EasyFoodOrder.Services.Order
{
    public interface IOrderService
    {
        int CreateOrder(IEnumerable<OrderItemModel> orderItems);
    }
}