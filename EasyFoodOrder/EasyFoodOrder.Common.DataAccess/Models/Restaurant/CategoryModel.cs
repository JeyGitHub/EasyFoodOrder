using System.Collections.Generic;

namespace EasyFoodOrder.Common.DataAccess.Models.Restaurant
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public IEnumerable<MenuItemModel> MenuItems { get; set; } = new List<MenuItemModel>();
    }
}