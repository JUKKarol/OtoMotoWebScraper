using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal static class Utilities
    {
        public static string CreateBaseUrl()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            IWebDriver driver = new ChromeDriver(/*options*/);


            driver.Navigate().GoToUrl("https://www.otomoto.pl");

            driver.FindElement(By.XPath("//div[contains(@role, 'alertdialog')]//button[contains(@id, 'accept')]")).Click();
            IWebElement submitButton = driver.FindElement(By.XPath("//form//button[@data-testid='submit-btn']"));


            IWebElement bodyTypeButton = driver.FindElement(By.XPath("//form//div[@id='filter_enum_body_type']//button"));
            bodyTypeButton.Click();

            IWebElement bodyTypeUl = driver.FindElement(By.XPath("//form//div[@id='filter_enum_body_type']//ul"));
            List<IWebElement> bodyTypeLiList = bodyTypeUl.FindElements(By.XPath("//form//div[@id='filter_enum_body_type']//ul/li")).ToList();
            bodyTypeLiList.RemoveRange(0, 1);
            bodyTypeLiList = bodyTypeLiList.Where(m => !m.GetAttribute("innerText").Contains("(0)")).ToList();

            int bodyTypeNumber = 1;
            foreach (var carBodyType in bodyTypeLiList)
            {
                Console.WriteLine($"{bodyTypeNumber}. {carBodyType.Text}");
                bodyTypeNumber++;
            }

            while (true)
            {
                try
                {
                    Console.Write($"Podaj nadwozie (1-{bodyTypeLiList.Count}): ");
                    int bodyTypeInput = int.Parse(Console.ReadLine());
                    bodyTypeLiList[bodyTypeInput - 1].Click();
                    break;
                }
                catch (Exception)
                {

                }
            }


            IWebElement brandButton = driver.FindElement(By.XPath("//form//div[@id='filter_enum_make']//button"));
            brandButton.Click();

            IWebElement brandUl = driver.FindElement(By.XPath("//form//div[@id='filter_enum_make']//ul"));
            List<IWebElement> brandLiList = brandUl.FindElements(By.XPath("//form//div[@id='filter_enum_make']//ul/li")).ToList();
            brandLiList.RemoveRange(0, 2);
            brandLiList = brandLiList.Where(m => !m.GetAttribute("innerText").Contains("(0)")).ToList();

            int brandNumber = 1;
            foreach (var carName in brandLiList)
            {
                Console.WriteLine($"{brandNumber}. {carName.Text}");
                brandNumber++;
            }

            while (true)
            {
                try
                {
                    Console.Write($"Podaj marke (1-{brandLiList.Count}): ");
                    int brandInput = int.Parse(Console.ReadLine());
                    brandLiList[brandInput - 1].Click();
                    break;
                }
                catch (Exception)
                {

                }
            }

            IWebElement modelButton = driver.FindElement(By.XPath("//form//div[@id='filter_enum_model']//button"));
            modelButton.Click();


            IWebElement modelUl = driver.FindElement(By.XPath("//form//div[@id='filter_enum_model']//ul"));
            List<IWebElement> modelLiList = modelUl.FindElements(By.XPath("//form//div[@id='filter_enum_model']//ul/li")).ToList();
            modelLiList.RemoveRange(0, 1);
            modelLiList = modelLiList.Where(m => !m.GetAttribute("innerText").Contains("(0)")).ToList();

            int modelNumber = 1;
            foreach (var carName in modelLiList)
            {
                Console.WriteLine($"{modelNumber}. {carName.Text}");
                modelNumber++;
            }

            while (true)
            {
                try
                {
                    Console.Write($"Podaj model (1-{modelLiList.Count}): ");
                    int modelInput = int.Parse(Console.ReadLine());
                    modelLiList[modelInput - 1].Click();
                    break;
                }
                catch (Exception)
                {

                }
            }


            IWebElement generationButton = driver.FindElement(By.XPath("//form//div[@id='filter_enum_generation']//button"));
            generationButton.Click();

            try
            {
                var tryGenerationList = driver.FindElement(By.XPath("//form//div[@id='filter_enum_generation']//ul"));
            }
            catch (Exception)
            {
                submitButton.Click();
                Thread.Sleep(1000);
                string urlNoGeneration = driver.Url;
                driver.Close();

                return urlNoGeneration;
            }

            IWebElement generationUl = driver.FindElement(By.XPath("//form//div[@id='filter_enum_generation']//ul"));
            List<IWebElement> generationLiList = generationUl.FindElements(By.XPath("//form//div[@id='filter_enum_generation']//ul/li")).ToList();
            generationLiList.RemoveRange(0, 1);
            generationLiList = generationLiList.Where(m => !m.GetAttribute("innerText").Contains("(0)")).ToList();

            int generationNumber = 1;
            foreach (var carName in generationLiList)
            {
                Console.WriteLine($"{generationNumber}. {carName.Text}");
                generationNumber++;
            }

            while (true)
            {
                try
                {
                    Console.Write($"Podaj generacje (1-{generationLiList.Count}): ");
                    int generationInput = int.Parse(Console.ReadLine());
                    generationLiList[generationInput - 1].Click();
                    break;
                }
                catch (Exception)
                {

                }
            }

            submitButton.Click();

            Thread.Sleep(1000);
            string url = driver.Url;
            driver.Close();

            return url;
        }

        public static string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[<>\(\)\!\?\-_]", string.Empty);
        }

        public static string TrimCSS(string input)
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

        public static string PrepareToIntParse(string input)
        {
            input = input.Trim();
            input = Regex.Replace(input, @"[^\d]", string.Empty);
            return input;
        }

        public static string EngineTrim(string input)
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

        public static void ShowCars(List<CarModel> carsModels)
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
