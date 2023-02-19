using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebScraper_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<CarModel> carsBasicInfo = new List<CarModel>();
            List<OfferModel> carsDetailsInfo = new List<OfferModel>();
            CarScraper carScraper = new CarScraper();
            OfferScraper offerScraper = new OfferScraper();

            carScraper.GetCars(ref carsBasicInfo);
            offerScraper.GetOffers(carsBasicInfo, ref carsDetailsInfo);
            carScraper.Merge(ref carsBasicInfo, carsDetailsInfo);
            Utilities.ShowCars(carsBasicInfo);
            Console.WriteLine($"Liczba aut: {carsBasicInfo.Count} w liście");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("░█████╗░████████╗░█████╗░███╗░░░███╗░█████╗░████████╗░█████╗░");
                Console.WriteLine("██╔══██╗╚══██╔══╝██╔══██╗████╗░████║██╔══██╗╚══██╔══╝██╔══██╗");
                Console.WriteLine("██║░░██║░░░██║░░░██║░░██║██╔████╔██║██║░░██║░░░██║░░░██║░░██║");
                Console.WriteLine("██║░░██║░░░██║░░░██║░░██║██║╚██╔╝██║██║░░██║░░░██║░░░██║░░██║");
                Console.WriteLine("╚█████╔╝░░░██║░░░╚█████╔╝██║░╚═╝░██║╚█████╔╝░░░██║░░░╚█████╔╝");
                Console.WriteLine("░╚════╝░░░░╚═╝░░░░╚════╝░╚═╝░░░░░╚═╝░╚════╝░░░░╚═╝░░░░╚════╝░");
                Console.WriteLine();
                Console.WriteLine("░██╗░░░░░░░██╗███████╗██████╗░░██████╗░█████╗░██████╗░░█████╗░██████╗░███████╗██████╗░");
                Console.WriteLine("░██║░░██╗░░██║██╔════╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗");
                Console.WriteLine("░╚██╗████╗██╔╝█████╗░░██████╦╝╚█████╗░██║░░╚═╝██████╔╝███████║██████╔╝█████╗░░██████╔╝");
                Console.WriteLine("░░████╔═████║░██╔══╝░░██╔══██╗░╚═══██╗██║░░██╗██╔══██╗██╔══██║██╔═══╝░██╔══╝░░██╔══██╗");
                Console.WriteLine("░░╚██╔╝░╚██╔╝░███████╗██████╦╝██████╔╝╚█████╔╝██║░░██║██║░░██║██║░░░░░███████╗██║░░██║");
                Console.WriteLine("░░░╚═╝░░░╚═╝░░╚══════╝╚═════╝░╚═════╝░░╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░░░░╚══════╝╚═╝░░╚═╝");

                Console.WriteLine("1. Samochody spełniające podane paramtry (%)");
                Console.WriteLine("2. Średnie ceny samochodów spełniające podane parametry");
                Console.WriteLine("3. Wycena samochodu");
                Console.WriteLine("4. Export do pliku");
                Console.WriteLine("X - wyjdź");


                string userInputString = Console.ReadLine();
                int userInput;
                if (userInputString == "X")
                {
                    Console.WriteLine("Do zobaczenia!");
                    return;
                }

                if (int.TryParse(userInputString, out userInput))
                {

                }
                else
                {
                    Console.WriteLine("Podaj liczbę");
                    continue;
                }

                switch (userInput)
                {
                    case 1:
                        MenuOptions.ChcekPercentOfCarsAchieveParameters(carsBasicInfo);
                        break;

                    case 2:
                        MenuOptions.ChcekAveragePriceOfCarsAchieveParameters(carsBasicInfo);
                        break;

                    case 3:
                        MenuOptions.YourCarEstimate(carsBasicInfo);
                        break;

                    case 4:
                        MenuOptions.ExpotToJSON(carsBasicInfo);
                        break;

                    default:
                        Console.WriteLine("Podaj Liczbe 1-4");
                        break;
                }
            }
        }
    }
}
