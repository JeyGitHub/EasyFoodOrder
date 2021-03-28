using System.Collections.Generic;

namespace EasyFoodOrder.Common.DataAccess.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public IEnumerable<MenuItemModel> MenuItems { get; set; } = new List<MenuItemModel>();
    }
}