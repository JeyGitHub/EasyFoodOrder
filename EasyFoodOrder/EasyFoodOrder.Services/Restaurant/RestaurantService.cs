using System.Collections.Generic;
using System.Linq;
using EasyFoodOrder.Common.DataAccess;
using EasyFoodOrder.Common.DataAccess.Models;
using EasyFoodOrder.Common.DataAccess.Models.Restaurant;

namespace EasyFoodOrder.Services.Restaurant
{
    public class RestaurantService: IRestaurantService
    {
        public IEnumerable<RestaurantModel> GetRestaurantData(string dish, string city)
        {
            var staticDataReader = new StaticDataReader();
            var restaurants = staticDataReader.ReadRestaurants();
            restaurants = FilterRestaurantData(restaurants, dish, city);
            return restaurants;
        }

        private IEnumerable<RestaurantModel> FilterRestaurantData(IEnumerable<RestaurantModel> restaurants, string dish, string city)
        {
            if (restaurants == null)
            {
                return new List<RestaurantModel>();
            }

            if (!string.IsNullOrEmpty(dish) && !string.IsNullOrEmpty(city))
            {
                dish = dish.ToLower().Trim();
                city = city.ToLower().Trim();
                restaurants = restaurants.Where(r => r.City.ToLower().Equals(city));
                var sortedRestaurants = new List<RestaurantModel>();
                foreach (var restaurant in restaurants)
                {
                    foreach (var category in restaurant.Categories)
                    {
                        if (category.Name.ToLower().Contains(dish))
                        {
                            restaurant.NumberOfOccurrences++;
                        }
                        else
                        {
                            category.Name = string.Empty;
                        }
                        
                        var menuItemsToRemove = new List<MenuItemModel>();
                        foreach (var menuItem in category.MenuItems)
                        {
                            if (menuItem.Name.ToLower().Contains(dish))
                            {
                                restaurant.NumberOfOccurrences++;
                            }
                            else
                            {
                                menuItemsToRemove.Add(menuItem);
                            }
                        }

                        if (menuItemsToRemove.Any())
                        {
                            var menuItemList = category.MenuItems.ToList();
                            foreach (var menuItemToRemove in menuItemsToRemove)
                            {
                                menuItemList.Remove(menuItemToRemove);
                            }

                            category.MenuItems = menuItemList;
                        }
                    }

                    var restaurantCategories = restaurant.Categories.ToList();
                    var categoriesToRemove = restaurant.Categories.Where(c => string.IsNullOrEmpty(c.Name) && !c.MenuItems.Any()).ToList();
                    foreach (var categoryToRemove in categoriesToRemove)
                    {
                        restaurantCategories.Remove(categoryToRemove);
                    }

                    restaurant.Categories = restaurantCategories;

                    if (restaurant.Categories.Any(c => !string.IsNullOrEmpty(c.Name) || c.MenuItems.Any()))
                    {
                        sortedRestaurants.Add(restaurant);
                    }
                }

                return sortedRestaurants
                    .Where(x => x.NumberOfOccurrences > 0)
                    .OrderByDescending(x => x.NumberOfOccurrences)
                    .ThenByDescending(x => x.Rank);
            }

            return restaurants.OrderByDescending(x => x.Rank);
        }
    }
}