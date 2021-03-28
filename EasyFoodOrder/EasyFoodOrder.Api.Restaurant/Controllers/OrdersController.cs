using System.Collections.Generic;
using EasyFoodOrder.Common.DataAccess.Models.Order;
using EasyFoodOrder.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyFoodOrder.Api.Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]IEnumerable<OrderItemModel> orderItems)
        {
            return Ok(_orderService.CreateOrder(orderItems));
        }
    }
}