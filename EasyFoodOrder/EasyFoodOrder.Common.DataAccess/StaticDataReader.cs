using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EasyFoodOrder.Common.DataAccess.Models;
using Newtonsoft.Json;

namespace EasyFoodOrder.Common.DataAccess
{
    public class StaticDataReader
    {
        public IEnumerable<RestaurantModel> ReadRestaurants()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string sampleDataPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), "Assets", "SampleData.json");
            string jsonData = File.ReadAllText(sampleDataPath);
            return JsonConvert.DeserializeObject<IEnumerable<RestaurantModel>>(jsonData);
        }
    }
}