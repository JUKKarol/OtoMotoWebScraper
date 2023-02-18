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
            bool electricSeat = false;
            bool heatedSeats = false;
            bool heatedBackSeats = false;
            bool massagedSeats = false;
            bool fullElectricWindows = false;
            bool bluetooth = false;
            bool cruiseControl = false;
            bool parktronic = false;
            bool multiWheel = false;

            var carScraper = new CarScraper();
            var carWeb = new HtmlWeb();

            try
            {
                var carDocument = carWeb.Load(link);
                var parametersTable = carDocument.QuerySelectorAll(".parametersArea li");
                var equipmentTable = carDocument.QuerySelectorAll(".offer-features__row li");

                price = int.Parse(Utilities.PrepareToIntParse(carDocument.QuerySelector(".offer-price__number").InnerText));
                if (carDocument.QuerySelector(".offer-price__currency").InnerText.Contains("EUR"))
                {
                    double priceDouble = Math.Round((price * 4.8) / 100, 0) * 100;
                    price = int.Parse(priceDouble.ToString());
                }

                horsePower = int.Parse(Utilities.PrepareToIntParse(parametersTable.Where(p => p.InnerText.Contains("Moc")).FirstOrDefault()?.InnerText));
                gearbox = (parametersTable.Where(p => p.InnerText.Contains("Skrzynia")).FirstOrDefault()?.InnerText).Replace("Skrzynia biegów", "").Trim();

                if (equipmentTable.Any(node => node.InnerText.Contains("Elektrycznie ustawiany fotel kierowcy")))
                {
                    electricSeat = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("Podgrzewany fotel kierowcy")))
                {
                    heatedSeats = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("Ogrzewane siedzenia tylne")))
                {
                    heatedBackSeats = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("masaż")))
                {
                    massagedSeats = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("Elektryczne szyby tylne")))
                {
                    fullElectricWindows = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("Bluetooth")))
                {
                    bluetooth = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("Tempomat")))
                {
                    cruiseControl = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("park")))
                {
                    parktronic = true;
                }

                if (equipmentTable.Any(node => node.InnerText.Contains("Kierownica wielofunkcyjna")))
                {
                    multiWheel = true;
                }
            }
            catch (Exception)
            {
                price = 0;
                horsePower = 0;
                gearbox = "";
            }


            carsDetailsInfo.Add(new OfferModel(price, horsePower, gearbox, electricSeat, heatedSeats, heatedBackSeats, massagedSeats, fullElectricWindows, bluetooth, cruiseControl, parktronic, multiWheel));
        }
    }
}
