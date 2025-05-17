using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakaz_travel.Menu;
using Zakaz_travel.Models;

namespace Zakaz_travel
{
    public static class OrderManagerUI
    {

        public static void ShowConfirmation( Order order )
        {
            LogoOrderManager.Zakaz();
            DateTime todayDate = DateTime.Today;
            DateTime futureDate = todayDate.AddDays( 3 );
            Console.WriteLine( $"{order.UserName}! Ваш заказ {order.ProductName} в количестве {order.Quantity} оформлен!" +
                $"\nОжидайте доставку по адресу {order.DeliveryAddress} к {futureDate:dd.MM.yyyy}. Ваш Zakaz-travel.com!" );
        }

        public static void ShowError( string message )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine( message );
            Console.ResetColor();
            Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
            Console.ReadKey();
        }

        public static void DisplayOrderScreen( Order order )
        {
            Console.Clear();
            LogoOrderManager.Zakaz();
            order.DisplayToConsole();
        }

        public static void FinalizeOrder( Order order )
        {
            OrderManagerUI.ShowConfirmation( order );
            Console.ReadKey();
            MenuManager.Menu();
        }
    }
}
