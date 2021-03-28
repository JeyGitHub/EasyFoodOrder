using System.Collections.Generic;
using EasyFoodOrder.Common.DataAccess.Models;

namespace EasyFoodOrder.Services.Restaurant
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantModel> GetRestaurantData(string dish, string city);
    }
}