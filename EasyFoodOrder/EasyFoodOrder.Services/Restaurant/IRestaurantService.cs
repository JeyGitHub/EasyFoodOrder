using System.Collections.Generic;
using EasyFoodOrder.Common.DataAccess.Models;
using EasyFoodOrder.Common.DataAccess.Models.Restaurant;

namespace EasyFoodOrder.Services.Restaurant
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantModel> GetRestaurantData(string dish, string city);
    }
}