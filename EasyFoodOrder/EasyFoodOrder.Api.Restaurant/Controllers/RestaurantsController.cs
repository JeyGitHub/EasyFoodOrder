using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyFoodOrder.Services.Restaurant;

namespace EasyFoodOrder.Api.Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> _logger;
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(ILogger<RestaurantsController> logger,
            IRestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string dish, [FromQuery]string city)
        {
            return Ok(_restaurantService.GetRestaurantData(dish, city));
        }
    }
}