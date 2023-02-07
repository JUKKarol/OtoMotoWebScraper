using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebScraper_01
{
    internal class CarScraper
    {
        private const string BaseUrl = "https://www.otomoto.pl/osobowe/ford/fiesta?search%5Bfilter_enum_generation%5D=gen-mk8-2017&search%5Bfilter_float_price%3Ato%5D=30000&search%5Badvanced_search_expanded%5D=true";

        

        public void GetCars(ref List<CarModel> carsModels)
        {
            var web = new HtmlWeb();
            var document = web.Load(BaseUrl);
            var CarOffers = document.QuerySelectorAll("main article");

            foreach (var CarOffer in CarOffers)
            {
                var name = CarOffer.QuerySelector("h2").InnerText;

                var lists = CarOffer.QuerySelectorAll("ul");
                var properties = lists[1].QuerySelectorAll("li");
                var offerInfo = lists[2].QuerySelectorAll("li");

                int year = int.Parse(properties[0].InnerText);
                int mileage = int.Parse(MileageTrim(properties[1].InnerText));
                double engineSize = double.Parse(EngineTrim(properties[2].InnerText));
                var fuelType = properties[3].InnerText;

                var localization = RemoveSpecialChars(TrimCSS(offerInfo[0].InnerText));
                var publicated = offerInfo[1].InnerText;

                var href = CarOffer.QuerySelector("h2 a").Attributes["href"].Value;
                //var carWeb = new HtmlWeb();
                //var carDocument = carWeb.Load(href);
                //var price = int.Parse(MileageTrim(carDocument.QuerySelector(".offer-price__number").InnerText));

                //var offerScraper = new OfferScraper();
                //var offerModel = offerScraper.GetOffers(href);

                int price = 3;

                if (properties[1].InnerText == offerInfo[1].InnerText)
                {
                    publicated = "no data";
                }

                carsModels.Add(new CarModel(name, price, year, mileage, engineSize, fuelType, localization, publicated, href));
            }

            
        }

        public void ShowCars(List<CarModel> carsModels)
        {
            foreach (var CarOffer in carsModels)
            {
                CarOffer.CarInfo();
            }
        }

        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[<>\(\)\!\?\-_]", string.Empty);
        }

        public string TrimCSS(string input)
        {
            int index = input.IndexOf("}");
            if (index == -1) return input;

            char nextChar = input[index + 1];
            if (Char.IsUpper(nextChar))
            {
                return input.Substring(index + 1);
            }
            else
            {
                return TrimCSS(input.Substring(index + 1));
            }
        }

        public string MileageTrim(string input)
        {
            input = input.Trim();
            input = Regex.Replace(input, @"[^\d]", string.Empty);
            return input;
        }

        public string EngineTrim(string input)
        {
            input = Regex.Replace(input, @"[^\d]", string.Empty);
            double inputDouble = double.Parse(input);
            inputDouble -= 3;
            inputDouble = inputDouble/10000;
            inputDouble = Math.Round(inputDouble, 1);
            return inputDouble.ToString();
        }
    }
}
