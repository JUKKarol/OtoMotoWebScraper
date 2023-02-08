using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<CarModel> carsBasicInfo = new List<CarModel>();
            List<OfferModel> carsDetailsInfo = new List<OfferModel>();
            CarScraper carScraper = new CarScraper();
            OfferScraper offerScraper = new OfferScraper();

            carScraper.GetCars(ref carsBasicInfo);
            offerScraper.GetOffers(carsBasicInfo, ref carsDetailsInfo);
            carScraper.Merge(ref carsBasicInfo, carsDetailsInfo);
            carScraper.ShowCars(carsBasicInfo);
            Console.WriteLine($"Liczba aut: {carsBasicInfo.Count} w liście");

            Console.ReadKey();
        }
    }
}
