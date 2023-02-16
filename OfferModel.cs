using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal class OfferModel
    {
        public OfferModel(int price, int horsePower, string gearbox)
        {
            Price = price;
            HorsePower = horsePower;
            Gearbox = gearbox;
        }

        public int Price { get; set; }
        public int HorsePower { get; set; }
        public string Gearbox { get; set; }

        public void OfferInfno()
        {
            Console.WriteLine($"Car Price: {Price}");
            Console.WriteLine($"Car Horse Power: {HorsePower}");
            Console.WriteLine($"Car GearBox: {Gearbox}");
        }
    }
}
