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
            int horsePower;
            string gearbox;

            var carScraper = new CarScraper();
            var carWeb = new HtmlWeb();

            try
            {
                var carDocument = carWeb.Load(link);
                var parametersTable = carDocument.QuerySelectorAll(".parametersArea li");
                horsePower = int.Parse(Utilities.MileageTrim(parametersTable.Where(p => p.InnerText.Contains("Moc")).FirstOrDefault()?.InnerText));
                gearbox = (parametersTable.Where(p => p.InnerText.Contains("Skrzynia")).FirstOrDefault()?.InnerText).Replace("Skrzynia biegów", "").Trim();

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
                horsePower = 0;
                gearbox = "";
            }

            
            carsDetailsInfo.Add(new OfferModel(price, horsePower, gearbox));
        }
    }
}
