using Zakaz_travel.Models;

class Program
{
    static void Main( string[] args )
    {
        Menu();
    }
    static void Menu()
    {
        Console.ResetColor();
        string[] options = { "Начать", "Помощь", "Выход" };
        Action[] actions =
        {
    StartWebsite,
    DisplayHelpMenu,
    () => { Environment.Exit(0); }
    };

        Action customCode = () =>
        {
            Console.Clear();
            Console.Title = "Zakaz-travel.com";
            Console.WriteLine( "Выполнил: Клыков Михаил." );
            Console.WriteLine( "Выберите действие:" );
        };

        CreateCustomMenu(
            menuItems: options,
            actions: actions,
            selectedColor: ConsoleColor.Magenta,
            title: null,
            customCodeBeforeRender: customCode
        );
    }

    static void StartWebsite()
    {
        var order = new Order( "", 0, "", "" ); // Знаю что так не пишут, но основная ошибка здесь. Вся информация в COMMENTS
        while ( true )
        {
            order.DisplayToConsole();
            order.ProductName = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( order.ProductName ) )
            {
                Console.WriteLine( "Ошибка: Название товара не может быть пустым. Пожалуйста, введите корректное название." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
                Console.ResetColor();
                Console.Clear();
            }
            else
            {
                break;
            }
        }
        int quantity;
        while ( true )
        {
            order.DisplayToConsole();
            string input = Console.ReadLine();

            if ( int.TryParse( input, out quantity ) && quantity > 0 )
            {
                order.Quantity = quantity;
                break;
            }
            else
            {
                Console.WriteLine( "Ошибка: Количество товара должно быть больше 0. Пожалуйста, введите корректное количество." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
                Console.ResetColor();
                Console.Clear();
            }
        }

        while ( true )
        {
            order.DisplayToConsole();
            Console.Write( "- Имя пользователя: " );
            order.UserName = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( order.UserName ) )
            {
                Console.WriteLine( "Ошибка: Имя пользователя не может быть пустым. Пожалуйста, введите корректное имя." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
                Console.ResetColor();
                Console.Clear();
            }
            else
            {
                break;
            }
        }

        while ( true )
        {
            order.DisplayToConsole();
            order.DeliveryAddress = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( order.DeliveryAddress ) )
            {
                Console.WriteLine( "Ошибка: Адрес доставки не может быть пустым. Пожалуйста, введите корректный адрес." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                break;
            }
        }

        Console.ResetColor();
        Console.WriteLine( $"Здравствуйте, {order.UserName}, вы заказали {order.Quantity} {order.ProductName} на адрес {order.DeliveryAddress}, все верно?" );
        string[] options = { "Да", "Нет" };
        Action[] actions =
        {
            () => {DateTime todayDate = DateTime.Today;
                   DateTime futureDate = todayDate.AddDays( 3 ); // +3 дня
                   Console.WriteLine( $"{order.UserName}! Ваш заказ {order.ProductName} в количестве {order.Quantity} оформлен!" +
                            $"\nОжидайте доставку по адресу {order.DeliveryAddress} к {futureDate:dd.MM.yyyy}. Ваш Zakaz-travel.com!" );
                    Console.ReadKey();
                    Menu(); },
            () => {var updatedOrder = Editor.EditOrder( order ); }
        };

        Action customCode = () =>
        {
        };

        CreateCustomMenu(
            menuItems: options,
            actions: actions,
            selectedColor: ConsoleColor.Magenta,
            title: null,
            customCodeBeforeRender: customCode
        );
        Console.ReadKey();
        return;
    }

    static void DisplayHelpMenu()
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
        Menu();
    }

    static void CreateCustomMenu(
    string[] menuItems,
    Action[] actions,
    ConsoleColor selectedColor = ConsoleColor.Green,
    ConsoleColor defaultColor = ConsoleColor.Gray,
    string title = null,
    bool loopMenu = true,
    string pointer = "> ",
    string unselectedPointer = "  ",
    Action customCodeBeforeRender = null )
    {
        if ( menuItems == null || menuItems.Length == 0 )
            throw new ArgumentException( "Кнопки не могут быть пустыми" );

        if ( actions != null && menuItems.Length != actions.Length )
            throw new ArgumentException( "Количество кнопок должно быть равно количеству методов" );

        int selectedIndex = 0;
        Console.CursorVisible = false;

        do
        {
            Console.Clear();

            // Выполнение дополнительного кода перед отрисовкой
            customCodeBeforeRender?.Invoke();

            // Вывод заголовка
            if ( !string.IsNullOrEmpty( title ) )
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine( title + "\n" );
                Console.ForegroundColor = defaultColor;
            }

            // Вывод пунктов меню
            for ( int i = 0; i < menuItems.Length; i++ )
            {
                if ( i == selectedIndex )
                {
                    Console.Write( pointer );
                    Console.ForegroundColor = selectedColor;
                }
                else
                {
                    Console.Write( unselectedPointer );
                    Console.ForegroundColor = defaultColor;
                }

                Console.WriteLine( menuItems[ i ] );
                Console.ResetColor();
            }

            // Обработка ввода
            var key = Console.ReadKey( true ).Key;

            switch ( key )
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = ( selectedIndex - 1 + menuItems.Length ) % menuItems.Length;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = ( selectedIndex + 1 ) % menuItems.Length;
                    break;
                case ConsoleKey.Enter:
                    if ( actions != null && actions.Length > selectedIndex )
                    {
                        Console.Clear();
                        actions[ selectedIndex ]?.Invoke();

                        if ( !loopMenu ) return;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        } while ( true );
    }
}
