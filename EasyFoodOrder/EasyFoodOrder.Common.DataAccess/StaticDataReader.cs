using System;
using System.IO;
using System.Reflection;

namespace EasyFoodOrder.Common.DataAccess
{
    public class StaticDataReader
    {
        public string GetData()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string sampleDataPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), "Assets", "SampleData.json");
            return File.ReadAllText(sampleDataPath);
        }
    }
}