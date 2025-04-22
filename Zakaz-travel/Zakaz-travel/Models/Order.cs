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
            if ( String.IsNullOrEmpty( ProductName ) )
            {
                Console.WriteLine( $"- Название товара: {ProductName}" );
            }
            if ( Quantity < 1 )
            {
                Console.WriteLine( $"- Количество товара : {Quantity}" );
            }
            if ( !String.IsNullOrEmpty( UserName ) )
            {
                Console.WriteLine( $"- Имя пользователя: {UserName}" );
            }
            if ( !String.IsNullOrEmpty( DeliveryAddress ) )
            {
                Console.WriteLine( $"- Адрес доставки: {DeliveryAddress}" );
            }
        }
        private void Decoration()
        {
            Console.BackgroundColor = ConsoleColor.Blue; ;
            Console.WriteLine( Editor.ZakazText );
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine( "\n                                             Оформление заказа" );
            Console.WriteLine( "                                           ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄" );
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }


    }
}
