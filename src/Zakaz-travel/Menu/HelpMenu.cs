using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakaz_travel.Models;

namespace Zakaz_travel.Menu
{
    public class HelpMenu
    {
        public static void DisplayHelpMenu()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine( $@"
                                                                                        
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
" );
            Console.ResetColor();
            Console.WriteLine( "\nНажмите любую клавишу для выхода..." );
            Console.ReadKey();
            Console.Clear();
            MenuManager.Menu();
        }
    }
}
