using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal class CarModel
    {
        public CarModel(string name, int price, int yearOfProduction, int mileageKM, double engineSize, string fuelType, string localization, string publicated, string link)
        {
            Name = name;
            Price = price;
            YearOfProduction = yearOfProduction;
            MileageKM = mileageKM;
            EngineSize = engineSize;
            FuelType = fuelType;
            Localization = localization;
            Publicated = publicated;
            Link = link;

            if (yearOfProduction.ToString() == localization)
            {
                Localization = "no data";
            }
        }

        public string Name{ get; set; }
        public int Price { get; set; }
        public int YearOfProduction { get; set; }
        public int MileageKM{ get; set; }
        public double EngineSize{ get; set; }
        public string FuelType{ get; set; }
        public string Localization { get; set; }
        public string Publicated { get; set; }
        public string Link { get; set; }

        public void CarInfo()
        {
            Console.WriteLine($"Nazwa: {Name}");
            Console.WriteLine($"Cena: {Price} PLN");
            Console.WriteLine($"Rocznik: {YearOfProduction}");
            Console.WriteLine($"Przebieg: {MileageKM} km");
            Console.WriteLine($"Pojemność skokowa: {EngineSize}L");
            Console.WriteLine($"Paliwo: {FuelType}");
            Console.WriteLine($"Lokalizacja: {Localization}");
            Console.WriteLine($"{Publicated}");
            Console.WriteLine($"link: {Link}");

            Console.WriteLine("------------------");
        }
    }
}
