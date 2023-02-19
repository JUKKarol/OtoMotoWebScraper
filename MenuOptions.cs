using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal static class MenuOptions
    {
        public static void ChcekPercentOfCarsAchieveParameters(List<CarModel> cars)
        {
            bool? electricSeat = null;
            bool? heatedSeats = null;
            bool? heatedBackSeats = null;
            bool? massagedSeats = null;
            bool? fullElectricWindows = null;
            bool? bluetooth = null;
            bool? cruiseControl = null;
            bool? parktronic = null;
            bool? multiWheel = null;
            int? horsePowerMin = null;
            int? horsePowerMax = null;

            GetInfoAboutRequiredParameters(ref electricSeat, ref heatedSeats, ref heatedBackSeats, ref massagedSeats, ref fullElectricWindows, ref bluetooth, ref cruiseControl, ref parktronic, ref multiWheel, ref horsePowerMin, ref horsePowerMax);

            List<CarModel> filtredCars = FilterCars(cars, electricSeat, heatedSeats, heatedBackSeats, massagedSeats, fullElectricWindows, bluetooth, cruiseControl, parktronic, multiWheel, horsePowerMin, horsePowerMax);

            Utilities.ShowCars(filtredCars);
            Console.ReadKey();
        }

        public static void ChcekAveragePriceOfCarsAchieveParameters(List<CarModel> cars)
        {
            bool? electricSeat = null;
            bool? heatedSeats = null;
            bool? heatedBackSeats = null;
            bool? massagedSeats = null;
            bool? fullElectricWindows = null;
            bool? bluetooth = null;
            bool? cruiseControl = null;
            bool? parktronic = null;
            bool? multiWheel = null;
            int? horsePowerMin = null;
            int? horsePowerMax = null;

            GetInfoAboutRequiredParameters(ref electricSeat, ref heatedSeats, ref heatedBackSeats, ref massagedSeats, ref fullElectricWindows, ref bluetooth, ref cruiseControl, ref parktronic, ref multiWheel, ref horsePowerMin, ref horsePowerMax);

            List<CarModel> filtredCars = FilterCars(cars, electricSeat, heatedSeats, heatedBackSeats, massagedSeats, fullElectricWindows, bluetooth, cruiseControl, parktronic, multiWheel, horsePowerMin, horsePowerMax);

            Utilities.ShowCars(filtredCars);
            Console.ReadKey();
        }

        public static void YourCarEstimate(List<CarModel> cars)
        {

        }

        public static void ExpotToJSON(List<CarModel> cars)
        {
            var json = JsonConvert.SerializeObject(cars);

            string projectFolderPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectFolderPath, "raport.json");

            File.WriteAllText(filePath, json);
        }

        public static void GetInfoAboutRequiredParameters(ref bool? electricSeat, ref bool? heatedSeats, ref bool? heatedBackSeats, ref bool? massagedSeats, ref bool? fullElectricWindows, ref bool? bluetooth, ref bool? cruiseControl, ref bool? parktronic, ref bool? multiWheel, ref int? horsePowerMin, ref int? horsePowerMax)
        {

            while (true)
            {
                Console.WriteLine("1. Elektryczne fotele: " + electricSeat);
                Console.WriteLine("2. Podgrzewane fotele: " + heatedSeats);
                Console.WriteLine("3. Podgrzewane tylne fotele: " + heatedBackSeats);
                Console.WriteLine("4. Masaż: " + massagedSeats);
                Console.WriteLine("5. Wszystkie szyby elektryczne: " + fullElectricWindows);
                Console.WriteLine("6. Bluetooth: " + bluetooth);
                Console.WriteLine("7. Tempomat: " + cruiseControl);
                Console.WriteLine("8. Parktronic: " + parktronic);
                Console.WriteLine("9. Kierownica multifunkcyjna: " + multiWheel);
                Console.Write("10. Konie mechaniczne (min-max): ");
                if (horsePowerMin != null)
                {
                    Console.Write(horsePowerMin);
                }
                else
                {
                    Console.Write("X");
                }
                Console.Write(" - ");
                if (horsePowerMax != null)
                {
                    Console.Write(horsePowerMax);
                }
                else
                {
                    Console.Write("X");
                }
                Console.WriteLine("\n0. Zatwierdź");

                string UserInputString = Console.ReadLine();
                int UserInput;
                try
                {
                    UserInput = int.Parse(UserInputString);
                }
                catch (Exception)
                {
                    Console.WriteLine("Podaj liczbę");
                    continue;
                }

                switch (UserInput)
                {
                    case 1:
                        if (electricSeat == null)
                        {
                            electricSeat = true;
                        }
                        else
                        {
                            electricSeat = !electricSeat;
                        }
                        Console.WriteLine("Elektryczne fotele: " + electricSeat);
                        break;
                    case 2:
                        if (heatedSeats == null)
                        {
                            heatedSeats = true;
                        }
                        else
                        {
                            heatedSeats = !heatedSeats;
                        }
                        Console.WriteLine("Podgrzewane fotele: " + heatedSeats);
                        break;
                    case 3:
                        if (heatedBackSeats == null)
                        {
                            heatedBackSeats = true;
                        }
                        else
                        {
                            heatedBackSeats = !heatedBackSeats;
                        }
                        Console.WriteLine("Podgrzewane tylne fotele: " + heatedBackSeats);
                        break;
                    case 4:
                        if (massagedSeats == null)
                        {
                            massagedSeats = true;
                        }
                        else
                        {
                            massagedSeats = !massagedSeats;
                        }
                        Console.WriteLine("Masaż: " + massagedSeats);
                        break;
                    case 5:
                        if (fullElectricWindows == null)
                        {
                            fullElectricWindows = true;
                        }
                        else
                        {
                            fullElectricWindows = !fullElectricWindows;
                        }
                        Console.WriteLine("Wszystkie szyby elektryczne: " + fullElectricWindows);
                        break;
                    case 6:
                        if (bluetooth == null)
                        {
                            bluetooth = true;
                        }
                        else
                        {
                            bluetooth = !bluetooth;
                        }
                        Console.WriteLine("Bluetooth: " + bluetooth);
                        break;
                    case 7:
                        if (cruiseControl == null)
                        {
                            cruiseControl = true;
                        }
                        else
                        {
                            cruiseControl = !cruiseControl;
                        }
                        Console.WriteLine("Tempomat: " + cruiseControl);
                        break;
                    case 8:
                        if (parktronic == null)
                        {
                            parktronic = true;
                        }
                        else
                        {
                            parktronic = !parktronic;
                        }
                        Console.WriteLine("Parktronic: " + parktronic);
                        break;
                    case 9:
                        if (multiWheel == null)
                        {
                            multiWheel = true;
                        }
                        else
                        {
                            multiWheel = !multiWheel;
                        }
                        Console.WriteLine("Kierownica multifunkcyjna: " + multiWheel);
                        break;
                    case 10:
                        Console.Write("Konie mechaniczne minimum: ");
                        try
                        {
                            horsePowerMin = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wprowadź poprawne dane");
                        }
                        Console.Write("Konie mechaniczne miaximum: ");
                        try
                        {
                            horsePowerMax = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wprowadź poprawne dane");
                        }
                        Console.WriteLine("Konie mechaniczne (min-max): " + horsePowerMin + "-" + horsePowerMax);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Podaj liczbę z zakresu");
                        break;
                }
            }

        }

        public static List<CarModel> FilterCars(List<CarModel> cars, bool? electricSeat, bool? heatedSeats, bool? heatedBackSeats, bool? massagedSeats, bool? fullElectricWindows, bool? bluetooth, bool? cruiseControl, bool? parktronic, bool? multiWheel, int? horsePowerMin, int? horsePowerMax)
        {
            List<CarModel> filteredCars = new List<CarModel>();

            foreach (CarModel car in cars)
            {
                if (electricSeat.HasValue && car.ElectricSeat != electricSeat.Value)
                {
                    continue;
                }
                if (heatedSeats.HasValue && car.HeatedSeats != heatedSeats.Value)
                {
                    continue;
                }
                if (heatedBackSeats.HasValue && car.HeatedBackSeats != heatedBackSeats.Value)
                {
                    continue;
                }
                if (massagedSeats.HasValue && car.MassagedSeats != massagedSeats.Value)
                {
                    continue;
                }
                if (fullElectricWindows.HasValue && car.FullElectricWindows != fullElectricWindows.Value)
                {
                    continue;
                }
                if (bluetooth.HasValue && car.Bluetooth != bluetooth.Value)
                {
                    continue;
                }
                if (cruiseControl.HasValue && car.CruiseControl != cruiseControl.Value)
                {
                    continue;
                }
                if (parktronic.HasValue && car.Parktronic != parktronic.Value)
                {
                    continue;
                }
                if (multiWheel.HasValue && car.MultiWheel != multiWheel.Value)
                {
                    continue;
                }
                if (horsePowerMin.HasValue && car.HorsePower < horsePowerMin.Value)
                {
                    continue;
                }
                if (horsePowerMax.HasValue && car.HorsePower > horsePowerMax.Value)
                {
                    continue;
                }

                filteredCars.Add(car);
            }

            return filteredCars;
        }
    }
}
