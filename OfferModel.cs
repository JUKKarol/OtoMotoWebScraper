using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_01
{
    internal class OfferModel
    {
        public OfferModel(int price, int horsePower, string gearbox, bool electricSeat, bool heatedSeats, bool heatedBackSeats, bool massagedSeats, bool fullElectricWindows, bool bluetooth, bool cruiseControl, bool parktronic, bool multiWheel)
        {
            Price = price;
            HorsePower = horsePower;
            Gearbox = gearbox;
            ElectricSeat = electricSeat;
            HeatedSeats = heatedSeats;
            HeatedBackSeats = heatedBackSeats;
            MassagedSeats = massagedSeats;
            FullElectricWindows = fullElectricWindows;
            Bluetooth = bluetooth;
            CruiseControl = cruiseControl;
            Parktronic = parktronic;
            MultiWheel = multiWheel;
        }

        public int Price { get; set; }
        public int HorsePower { get; set; }
        public string Gearbox { get; set; }
        public bool ElectricSeat { get; set; }
        public bool HeatedSeats { get; set; }
        public bool HeatedBackSeats { get; set; }
        public bool MassagedSeats { get; set; }
        public bool FullElectricWindows { get; set; }
        public bool Bluetooth { get; set; }
        public bool CruiseControl { get; set; }
        public bool Parktronic { get; set; }
        public bool MultiWheel { get; set; }

        public void OfferInfno()
        {
            Console.WriteLine($"Car Price: {Price}");
            Console.WriteLine($"Car Horse Power: {HorsePower}");
            Console.WriteLine($"Car GearBox: {Gearbox}");
        }
    }
}
