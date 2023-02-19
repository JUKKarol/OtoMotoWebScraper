using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal static class MenuOptions
    {
        public static void ChcekPercentOfCarsAchieveParameters(List<CarModel> cars)
        {

        }

        public static void ChcekAveragePriceOfCarsAchieveParameters(List<CarModel> cars)
        {

        }

        public static void YourCarEstimate(List<CarModel> cars)
        {

        }

        public static void ExpotToJSON(List<CarModel> cars)
        {
            var json = JsonConvert.SerializeObject(cars);

            string projectFolderPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectFolderPath, "raport.json");

            File.WriteAllText(filePath, json);
        }
    }
}
