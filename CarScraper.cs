using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace WebScraper_01
{
    internal class CarScraper
    {
        private static string CreateUrl = Utilities.CreateBaseUrl();
        private string BaseUrl = CreateUrl;


        public void GetCars(ref List<CarModel> carsModels)
        {
            var web = new HtmlWeb();
            var document = web.Load(BaseUrl);
            bool isNextPage = true;
            var paginationList = document.QuerySelectorAll(".pagination-list li span");
            int lastPageNumber = 1;
            int actualPageNumber = 1;

            try
            {
                lastPageNumber = int.Parse(paginationList[paginationList.Count - 1].InnerText);
            }
            catch (Exception)
            {

            }

            while (isNextPage)
            {
                var CarOffers = document.QuerySelectorAll("main article");

                foreach (var CarOffer in CarOffers)
                {
                    try
                    {
                        var name = CarOffer.QuerySelector("h2").InnerText;

                        var lists = CarOffer.QuerySelectorAll("ul");
                        var properties = lists[1].QuerySelectorAll("li");
                        var offerInfo = lists[2].QuerySelectorAll("li");

                        int year = int.Parse(properties[0].InnerText);
                        int mileage = int.Parse(MileageTrim(properties[1].InnerText));
                        double engineSize = double.Parse(EngineTrim(properties[2].InnerText));
                        string fuelType = "";

                        try
                        {
                            fuelType = properties[3].InnerText;
                        }
                        catch (Exception)
                        {
                            fuelType = "Brak danych";
                        }

                        var localization = RemoveSpecialChars(TrimCSS(offerInfo[0].InnerText));
                        var publicated = offerInfo[1].InnerText;
                        var href = CarOffer.QuerySelector("h2 a").Attributes["href"].Value;

                        int price = 0;

                        if (properties[1].InnerText == offerInfo[1].InnerText)
                        {
                            publicated = "no data";
                        }

                        carsModels.Add(new CarModel(name, price, year, mileage, engineSize, fuelType, localization, publicated, href));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    
                }
                actualPageNumber++;
                NextPage(ref document, ref isNextPage, lastPageNumber, ref actualPageNumber);
            }
        }

        public void Merge(ref List<CarModel> carsBasicInfo, List<OfferModel> carsDetailsInfo)
        {
            for (int i = 0; i < carsBasicInfo.Count; i++)
            {
                carsBasicInfo[i].Price = carsDetailsInfo[i].Price;
            }
        }

        public void NextPage(ref HtmlDocument document, ref bool isPageNext, int lastPageNumber, ref int actualPageNumber)
        {
            try
            {
                if (actualPageNumber > lastPageNumber)
                {
                    throw new Exception();
                }
                var web = new HtmlWeb();
                string href = BaseUrl + "?page=" + actualPageNumber.ToString();
                document = web.Load(href);
            }
            catch (Exception)
            {
                isPageNext = false;
            }
        }

        public void ShowCars(List<CarModel> carsModels)
        {
            int i = 1;
            foreach (var CarOffer in carsModels)
            {
                Console.WriteLine($"Number: {i}");
                i++;
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
            try
            {
                input = Regex.Replace(input, @"[^\d]", string.Empty);
                double inputDouble = double.Parse(input);
                inputDouble -= 3;
                inputDouble = inputDouble / 10000;
                inputDouble = Math.Round(inputDouble, 1);
                return inputDouble.ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
