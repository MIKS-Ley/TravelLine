using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakaz_travel.Models
{
    public class Order
    {
        public Order( string productName, int quantity, string userName, string deliveryAddress )
        {
            ProductName = productName;
            Quantity = quantity;
            UserName = userName;
            DeliveryAddress = deliveryAddress;
        }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public string? UserName { get; set; }
        public string? DeliveryAddress { get; set; }

        public void DisplayToConsole()
        {
            Decoration();
            if ( ProductName == default )
            {
                Console.WriteLine( $"- Название товара: {ProductName}" );
            }
            if ( Quantity == default )
            {
                Console.WriteLine( $"- Название : {Quantity}" );
            }
            if ( UserName == default )
            {
                Console.WriteLine( $"- Название товара: {UserName}" );
            }
            if ( DeliveryAddress == default )
            {
                Console.WriteLine( $"- Название товара: {DeliveryAddress}" );
            }
        }
        private void Decoration()
        {
            Console.Clear();
            Console.ResetColor();
            SiteName();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine( "\n                                             Оформление заказа" );
            Console.WriteLine( "                                           ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄" );
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        private void SiteName()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine( $@"

 /$$$$$$$$           /$$                                     /$$                                              /$$
|_____ $$           | $$                                    | $$                                             | $$
     /$$/   /$$$$$$ | $$   /$$  /$$$$$$  /$$$$$$$$         /$$$$$$    /$$$$$$   /$$$$$$  /$$    /$$  /$$$$$$ | $$
    /$$/   |____  $$| $$  /$$/ |____  $$|____ /$$/ /$$$$$$|_  $$_/   /$$__  $$ /$$__  $$|  $$  /$$/ |____  $$| $$
   /$$/     /$$$$$$$| $$$$$$/   /$$$$$$$   /$$$$/ |______/  | $$    | $$  \__/| $$$$$$$$ \  $$/$$/   /$$$$$$$| $$
  /$$/     /$$__  $$| $$_  $$  /$$__  $$  /$$__/            | $$ /$$| $$      | $$_____/  \  $$$/   /$$__  $$| $$
 /$$$$$$$$|  $$$$$$$| $$ \  $$|  $$$$$$$ /$$$$$$$$          |  $$$$/| $$      |  $$$$$$$   \  $/   |  $$$$$$$| $$
|________/ \_______/|__/  \__/ \_______/|________/           \___/  |__/       \_______/    \_/     \_______/|__/" );
            Console.ResetColor();
        }

    }
}
