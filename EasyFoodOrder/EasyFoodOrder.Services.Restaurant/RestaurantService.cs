using System;
using EasyFoodOrder.Common.DataAccess;

namespace EasyFoodOrder.Services.Restaurant
{
    public class RestaurantService: IRestaurantService
    {
        public string GetData()
        {
            var staticDataReader = new StaticDataReader();
            return staticDataReader.GetData();
        }
    }
}