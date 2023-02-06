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
            CarScraper carScraper = new CarScraper();
            carScraper.GetCars();
            carScraper.ShowCars();
            Console.ReadKey();
        }
    }
}
