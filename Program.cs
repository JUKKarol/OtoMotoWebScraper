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
            carScraper.GetCars(ref carsBasicInfo);

            for (int i = 0; i < carsBasicInfo.Count; i++)
            {
                OfferScraper offerScraper = new OfferScraper();
                //Console.WriteLine(carsBasicInfo[i].Link);
                offerScraper.GetOffers(carsBasicInfo[i].Link, ref carsDetailsInfo);
                //Console.WriteLine(carsDetailsInfo[i].Price);
            }

            for (int i = 0; i < carsBasicInfo.Count; i++)
            {
                carsDetailsInfo[i].OfferInfno();
                carsBasicInfo[i].CarInfo();
            }

            Console.ReadKey();
        }
    }
}
