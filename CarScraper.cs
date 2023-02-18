using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading;
using OpenQA.Selenium.Support.UI;




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
                document = web.Load(BaseUrl);
                Console.WriteLine(BaseUrl);
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
                        int mileage = int.Parse(Utilities.PrepareToIntParse(properties[1].InnerText));
                        double engineSize = double.Parse(Utilities.EngineTrim(properties[2].InnerText));
                        string fuelType = "";

                        try
                        {
                            fuelType = properties[3].InnerText;
                        }
                        catch (Exception)
                        {
                            fuelType = "Brak danych";
                        }

                        var localization = Utilities.RemoveSpecialChars(Utilities.TrimCSS(offerInfo[0].InnerText));
                        var publicated = offerInfo[1].InnerText;
                        var href = CarOffer.QuerySelector("h2 a").Attributes["href"].Value;

                        if (properties[1].InnerText == offerInfo[1].InnerText)
                        {
                            publicated = "no data";
                        }

                        carsModels.Add(new CarModel(name, year, mileage, engineSize, fuelType, localization, publicated, href));
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }
                NextPage(ref document, ref isNextPage, lastPageNumber, ref actualPageNumber);
                actualPageNumber++;
            }
        }

        public void Merge(ref List<CarModel> carsBasicInfo, List<OfferModel> carsDetailsInfo)
        {
            for (int i = 0; i < carsBasicInfo.Count; i++)
            {
                carsBasicInfo[i].Price = carsDetailsInfo[i].Price;
                carsBasicInfo[i].HorsePower = carsDetailsInfo[i].HorsePower;
                carsBasicInfo[i].Gearbox = carsDetailsInfo[i].Gearbox;
                carsBasicInfo[i].ElectricSeat = carsDetailsInfo[i].ElectricSeat;
                carsBasicInfo[i].HeatedSeats = carsDetailsInfo[i].HeatedSeats;
                carsBasicInfo[i].HeatedBackSeats = carsDetailsInfo[i].HeatedBackSeats;
                carsBasicInfo[i].MassagedSeats = carsDetailsInfo[i].MassagedSeats;
                carsBasicInfo[i].FullElectricWindows = carsDetailsInfo[i].FullElectricWindows;
                carsBasicInfo[i].Bluetooth = carsDetailsInfo[i].Bluetooth;
                carsBasicInfo[i].CruiseControl = carsDetailsInfo[i].CruiseControl;
                carsBasicInfo[i].Parktronic = carsDetailsInfo[i].Parktronic;
                carsBasicInfo[i].MultiWheel = carsDetailsInfo[i].MultiWheel;
            }
        }

        public void NextPage(ref HtmlDocument document, ref bool isPageNext, int lastPageNumber, ref int actualPageNumber)
        {
            try
            {
                Thread.Sleep(1000);
                if (actualPageNumber >= lastPageNumber)
                {
                    throw new Exception();
                }

                if (actualPageNumber == 1)
                {
                    var options = new ChromeOptions();
                    options.AddArgument("--headless");

                    IWebDriver driver = new ChromeDriver(/*options*/);
                    driver.Navigate().GoToUrl(BaseUrl);

                    Thread.Sleep(1000);
                    driver.FindElement(By.XPath("//div[contains(@role, 'alertdialog')]//button[contains(@id, 'accept')]")).Click();
                    Thread.Sleep(1000);

                    IWebElement element = driver.FindElement(By.CssSelector("ul.pagination-list li[title*='Next'] svg"));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                    js.ExecuteScript("window.scrollBy(0, -300)");
                    Thread.Sleep(1000);
                    driver.FindElement(By.CssSelector("ul.pagination-list li[title*='Next'] svg")).Click();

                    Thread.Sleep(1000);
                    string url = driver.Url;
                    BaseUrl = url;
                    Thread.Sleep(1000);

                    driver.Close();
                    Thread.Sleep(1000);

                    var web = new HtmlWeb();
                    document = web.Load(url);
                }
                else
                {
                    BaseUrl = BaseUrl.Replace($"page={actualPageNumber}", $"page={actualPageNumber + 1}");
                    var web = new HtmlWeb();
                    document = web.Load(BaseUrl);
                    Console.WriteLine($"Page number: {actualPageNumber}, link: {BaseUrl}");
                }
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
    }
}
