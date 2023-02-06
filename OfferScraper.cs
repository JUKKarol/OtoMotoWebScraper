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
        public OfferModel GetOffers(string link)
        {
            var _autoScraper = new CarScraper();

            var carWeb = new HtmlWeb();
            var carDocument = carWeb.Load(link);
            var price = int.Parse(_autoScraper.MileageTrim(carDocument.QuerySelector(".offer-price__number").InnerText));

            return new OfferModel(price);
        }
    }
}
