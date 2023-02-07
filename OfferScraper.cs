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
        public void GetOffers(string link, ref List<OfferModel> carsDetailsInfo)
        {
            int price = 0;
            try
            {
                var _autoScraper = new CarScraper();

                var carWeb = new HtmlWeb();
                var carDocument = carWeb.Load(link);
                price = int.Parse(_autoScraper.MileageTrim(carDocument.QuerySelector(".offer-price__number").InnerText));
            }
            catch (Exception)
            {
                price = 999;
            }

            carsDetailsInfo.Add(new OfferModel(price));
        }
    }
}
