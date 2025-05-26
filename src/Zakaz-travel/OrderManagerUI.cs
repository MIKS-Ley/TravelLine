using Zakaz_travel.Models;

namespace Zakaz_travel
{
    public static class OrderManagerUI
    {
        public const string ZakazText = @"
 /$$$$$$$$           /$$                                     /$$                                              /$$
|_____ $$           | $$                                    | $$                                             | $$
     /$$/   /$$$$$$ | $$   /$$  /$$$$$$  /$$$$$$$$         /$$$$$$    /$$$$$$   /$$$$$$  /$$    /$$  /$$$$$$ | $$
    /$$/   |____  $$| $$  /$$/ |____  $$|____ /$$/ /$$$$$$|_  $$_/   /$$__  $$ /$$__  $$|  $$  /$$/ |____  $$| $$
   /$$/     /$$$$$$$| $$$$$$/   /$$$$$$$   /$$$$/ |______/  | $$    | $$  \__/| $$$$$$$$ \  $$/$$/   /$$$$$$$| $$
  /$$/     /$$__  $$| $$_  $$  /$$__  $$  /$$__/            | $$ /$$| $$      | $$_____/  \  $$$/   /$$__  $$| $$
 /$$$$$$$$|  $$$$$$$| $$ \  $$|  $$$$$$$ /$$$$$$$$          |  $$$$/| $$      |  $$$$$$$   \  $/   |  $$$$$$$| $$
|________/ \_______/|__/  \__/ \_______/|________/           \___/  |__/       \_______/    \_/     \_______/|__/";

        public static void DisplayHelpMenu()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($@"
                                                                                        
                                      Техподдержка.                                     
                                                                                        
Пункт 2.11.                        Оформления заказа.                                   
                                                                                        
Здравствуйте! В данной программе вы можете заказать товары с магазина Zakaz - travel.com
Для этого вам нужно указать: название товара, его количество, ваше имя и адрес доставки.
Пример предоставлен ниже.                                                               
                                                                                        
    - Название товара: Сумка                                                            
    - Количество товара: 1                                                              
    - Имя пользователя: Мария                                                           
    - Адрес доставки: г. Москва ул. Ленина д. 18 кв. 4                                  
                                                                                        
После произойдет подтверждение заказа, и ваш товар будет доставлен в ближайшие время.   
                                                                                        
                                           С наилучшими пожеланиями ваш Zakaz-travel!   
");
            Console.ResetColor();
            Console.WriteLine( "\nНажмите любую клавишу для выхода..." );
            Console.ReadKey();
            Console.Clear();
            MenuManager.Menu();
        }

        public static void Zakaz()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine( ZakazText );
            Console.ResetColor();
        }

        public static void ShowConfirmation( Order order )
        {
            Zakaz();
            DateTime todayDate = DateTime.Today;
            DateTime futureDate = todayDate.AddDays( 3 );
            Console.WriteLine( $"{ order.UserName }! Ваш заказ { order.ProductName } в количестве { order.Quantity } оформлен!" +
                $"\nОжидайте доставку по адресу { order.DeliveryAddress } к {futureDate:dd.MM.yyyy}. Ваш Zakaz-travel.com!" );
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
            Zakaz();
            order.DisplayToConsole();
        }

        public static void FinalizeOrder( Order order )
        {
            ShowConfirmation( order );
            Console.ReadKey();
            MenuManager.Menu();
        }
    }
}
