using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebScraper_01
{
    internal class OfferScraper
    {
        public void GetOffers(List<CarModel> carsBasicInfo, ref List<OfferModel> carsDetailsInfo)
        {
            for (int i = 0; i < carsBasicInfo.Count; i++)
            {
                OfferScraper offerScraper = new OfferScraper();

                offerScraper.GetOffer(carsBasicInfo[i].Link, ref carsDetailsInfo);
            }
        }

        public void GetOffer(string link, ref List<OfferModel> carsDetailsInfo)
        {
            int price;

            try
            {
                var carScraper = new CarScraper();
                var carWeb = new HtmlWeb();
                var carDocument = carWeb.Load(link);

                price = int.Parse(Utilities.MileageTrim(carDocument.QuerySelector(".offer-price__number").InnerText));
                if (carDocument.QuerySelector(".offer-price__currency").InnerText.Contains("EUR"))
                {
                    double priceDouble = Math.Round((price*4.8)/100, 0)*100;
                    price = int.Parse(priceDouble.ToString());
                }
            }
            catch (Exception)
            {
                price = 0;
            }

            carsDetailsInfo.Add(new OfferModel(price));
        }
    }
}
