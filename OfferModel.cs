using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal class OfferModel
    {
        public OfferModel(int price)
        {
            Price = price;
        }

        public int Price { get; set; }

        public void OfferInfno()
        {
            Console.WriteLine($"Car Price: {Price}");
        }
    }
}
