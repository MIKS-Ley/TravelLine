class Program
{
    static void Main()
    {
        string[] menu = { "Начать", "Помощь", "Выход" };
        int select = 0;
        while ( true ) // Тело программы
        {
            Console.Clear();
            Console.Title = "Zakaz-travel.com";
            Console.WriteLine( "Выполнил: Клыков Михаил." );
            Console.WriteLine( "Выберите действие:" );

            for ( int i = 0; i < menu.Length; i++ )
            {
                if ( i == select )
                {
                    Console.Write( "> " );
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.Write( "  " );
                }

                Console.WriteLine( menu[ i ] );
                Console.ResetColor();
            }

            var key = Console.ReadKey( true ).Key;

            if ( key == ConsoleKey.UpArrow && select > 0 ) select--;
            if ( key == ConsoleKey.DownArrow && select < menu.Length - 1 ) select++;

            if ( key == ConsoleKey.Enter )
            {
                if ( select == 0 ) StartWebsite();
                if ( select == 1 ) DisplayHelpMenu();
                if ( select == 2 ) return;
            }
        }
    }

    static void StartWebsite() //Первоночальное окно оформление заказа
    {
        string? nameProduct = string.Empty;
        int quantity = 0;
        string? nameUser = string.Empty;
        string? deliveryAddress = string.Empty;

        while ( true )
        {
            Decoration();
            Console.Write( $"- Название товара: " );
            nameProduct = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( nameProduct ) )
            {
                Console.WriteLine( "Ошибка: Название товара не может быть пустым. Пожалуйста, введите корректное название." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
            }
            else
            {
                break;
            }
        }

        while ( true )
        {
            Decoration();
            Console.WriteLine( $"- Название товара: {nameProduct}" );
            Console.Write( "- Количество товара: " );
            string? quantityInput = Console.ReadLine();

            if ( int.TryParse( quantityInput, out quantity ) && quantity > 0 )
            {
                break;
            }
            else
            {
                Console.WriteLine( "Ошибка: Количество товара должно быть больше 0. Пожалуйста, введите корректное количество." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
            }
        }

        while ( true )
        {
            Decoration();
            Console.WriteLine( $"- Название товара: {nameProduct}" );
            Console.WriteLine( $"- Количество товара: {quantity}" );
            Console.Write( "- Имя пользователя: " );
            nameUser = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( nameUser ) )
            {
                Console.WriteLine( "Ошибка: Имя пользователя не может быть пустым. Пожалуйста, введите корректное имя." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
            }
            else
            {
                break;
            }
        }

        while ( true )
        {
            Decoration();
            Console.WriteLine( $"- Название товара: {nameProduct}" );
            Console.WriteLine( $"- Количество товара: {quantity}" );
            Console.WriteLine( $"- Имя пользователя: {nameUser}" );
            Console.Write( "- Адрес доставки: " );
            deliveryAddress = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( deliveryAddress ) )
            {
                Console.WriteLine( "Ошибка: Адрес доставки не может быть пустым. Пожалуйста, введите корректный адрес." );
                Console.WriteLine( "Нажмите любую клавишу для повторного ввода..." );
                Console.ReadKey();
            }
            else
            {
                break;
            }
        }

        Console.ResetColor();
        string quantityProduct = quantity.ToString();
        Console.WriteLine( $"Здравствуйте, {nameUser}, вы заказали {quantityProduct} {nameProduct} на адрес {deliveryAddress}, все верно?" );
        string[] menu = { "Да", "Нет" };
        int select = 0;
        int menuTopPosition = Console.CursorTop; // Запоминаем начальную позицию меню

        while ( true )
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            Console.SetCursorPosition( 0, menuTopPosition );

            for ( int i = 0; i < menu.Length; i++ )
            {
                Console.Write( new string( ' ', Console.WindowWidth ) );
                Console.SetCursorPosition( 0, menuTopPosition + i );

                if ( i == select )
                {
                    Console.Write( "> " );
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.Write( "  " );
                }

                Console.Write( menu[ i ] );
                Console.ResetColor();
            }

            Console.SetCursorPosition( currentLeft, currentTop );

            var key = Console.ReadKey( true ).Key;

            if ( key == ConsoleKey.UpArrow && select > 0 ) select--;
            if ( key == ConsoleKey.DownArrow && select < menu.Length - 1 ) select++;
            if ( key == ConsoleKey.LeftArrow ) select = 0;
            if ( key == ConsoleKey.RightArrow ) select = 1;

            if ( key == ConsoleKey.Enter )
            {
                Console.SetCursorPosition( 0, menuTopPosition );
                for ( int i = 0; i < menu.Length; i++ )
                {
                    Console.Write( new string( ' ', Console.WindowWidth ) );
                    if ( i < menu.Length - 1 )
                        Console.SetCursorPosition( 0, menuTopPosition + i + 1 );
                }

                Console.SetCursorPosition( 0, menuTopPosition );

                if ( select == 0 ) // если да
                {
                    DateTime todayDate = DateTime.Today;
                    DateTime futureDate = todayDate.AddDays( 3 ); // +3 дня
                    Console.WriteLine( $"{nameUser}! Ваш заказ {nameProduct} в количестве {quantityProduct} оформлен!" +
                            $"\nОжидайте доставку по адресу {deliveryAddress} к {futureDate:dd.MM.yyyy}. Ваш Zakaz-travel.com!" );
                }
                else // иначе нет
                {
                    var updatedOrder = Editor.EditOrder( nameProduct, quantityProduct, nameUser, deliveryAddress );
                }

                Console.ReadKey();
                return;
            }
        }
    }

    static void DisplayHelpMenu() // Решил добавить информацию для корректного использование программы 
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
        StartWebsite();
    }

    static void Decoration()
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

    static void SiteName()
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

public static class Editor  //Сначала делал без класса , но слишком грамосткий получился и проблемы могут быть, поэтому так 
{
    static void Zakaz()
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

    public static (string name, string quantity, string user, string address) EditOrder(
        string nameProduct, string quantityProduct, string nameUser, string deliveryAddress )
    {
        string[] parameter = {
            "    - Название товара",
            "    - Количество товара",
            "    - Имя пользователя",
            "    - Адрес доставки",
            "    - Оформить заказ"
        };

        int field = 0; //Буфер

        while ( true )
        {
            Zakaz();
            Console.WriteLine( "Редактирование данных заказа:" );
            Console.WriteLine( "Вверх/Вниз - выбор пункта | Enter - выбрать" );
            Console.WriteLine( "--------------------------------------------------" );

            for ( int i = 0; i < parameter.Length; i++ ) // Подсветка нажатие не чтоб по красоте :)
            {
                if ( i == field )
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write( "> " );
                }
                else
                {
                    Console.Write( "  " );
                }

                if ( i < parameter.Length - 1 )
                {
                    string meaning = Value( i, nameProduct, quantityProduct, nameUser, deliveryAddress );
                    Console.WriteLine( $"{parameter[ i ]}: {meaning}" );
                }
                else
                {
                    Console.WriteLine( $"{parameter[ i ]}" ); //Для "оформить заказ"
                }
                Console.ResetColor();
            }

            var key = Console.ReadKey( true );

            switch ( key.Key )
            {
                case ConsoleKey.Escape:
                    return (nameProduct, quantityProduct, nameUser, deliveryAddress);

                case ConsoleKey.UpArrow when field > 0:
                    field--;
                    break;

                case ConsoleKey.DownArrow when field < parameter.Length - 1:
                    field++;
                    break;

                case ConsoleKey.Enter:
                    if ( field == parameter.Length - 1 )
                    {
                        Console.Clear();
                        TheEnd( nameProduct, quantityProduct, nameUser, deliveryAddress );
                        Console.WriteLine( "\nНажмите любую клавишу для завершения..." );
                        Console.ReadKey();
                        return (nameProduct, quantityProduct, nameUser, deliveryAddress);
                    }
                    else
                    {
                        string newValue = EditField( field, parameter, nameProduct, quantityProduct, nameUser, deliveryAddress );

                        switch ( field ) // - это тут обновляю поле
                        {
                            case 0: nameProduct = newValue; break;
                            case 1: quantityProduct = newValue; break;
                            case 2: nameUser = newValue; break;
                            case 3: deliveryAddress = newValue; break;
                        }
                    }
                    break;
            }
        }
    }

    private static void TheEnd( string product, string count, string name, string address )
    {
        Zakaz();
        DateTime todayDate = DateTime.Today;
        DateTime futureDate = todayDate.AddDays( 3 ); // +3 дня
        Console.WriteLine( $"{name}! Ваш заказ {product} в количестве {count} оформлен!" +
            $"\nОжидайте доставку по адресу {address} к {futureDate:dd.MM.yyyy}. Ваш Zakaz-travel.com!\"" );
    }

    private static string Value( int fieldIndex, string name, string qty, string customer, string address ) //получает
    {
        return fieldIndex switch
        {
            0 => name,
            1 => qty,
            2 => customer,
            3 => address,
            _ => ""
        };
    }

    private static string EditField( int fieldIndex, string[] fields, string name,
        string qty, string customer, string address ) // редактирует 
    {
        string currentValue = fieldIndex switch
        {
            0 => name,
            1 => qty,
            2 => customer,
            3 => address,
            _ => ""
        };

        Zakaz();
        Console.WriteLine( $"РЕДАКТИРОВАНИЕ: {fields[ fieldIndex ].Trim()}" );
        Console.WriteLine( "--------------------------------------------------" );
        Console.WriteLine( $"Текущее значение: {currentValue}" );
        Console.WriteLine( "Введите новое значение (или нажмите Enter чтобы оставить текущее):" );
        Console.Write( "> " );

        string? newValue = Console.ReadLine();
        if ( fieldIndex == 1 )
        {
            if ( string.IsNullOrWhiteSpace( newValue ) )
            {
                return currentValue;
            }

            if ( int.TryParse( newValue, out int parsedQty ) && parsedQty >= 0 )
            {
                return parsedQty.ToString();
            }
            else
            {
                bool validQtyInput = false;
                while ( !validQtyInput )
                {
                    Console.Clear();
                    Zakaz();
                    Console.WriteLine( $"РЕДАКТИРОВАНИЕ: {fields[ fieldIndex ].Trim()}" );
                    Console.WriteLine( "--------------------------------------------------" );
                    Console.WriteLine( $"Текущее значение: {currentValue}" );
                    Console.WriteLine( "Введите новое значение (или нажмите Enter чтобы оставить текущее):" );
                    Console.Write( "> " );
                    string? QtyInput = Console.ReadLine();

                    if ( string.IsNullOrWhiteSpace( QtyInput ) )
                    {
                        return currentValue;
                    }

                    if ( int.TryParse( QtyInput, out int Qty ) && Qty >= 0 )
                    {
                        currentValue = Qty.ToString();
                        validQtyInput = true;
                    }
                    else
                    {
                        Console.WriteLine( "Неверный ввод. Пожалуйста, введите целое положительное число." );
                    }
                }
                return currentValue;
            }
        }
        else if ( string.IsNullOrWhiteSpace( newValue ) )
        {
            return currentValue;
        }
        else
        {
            return newValue;
        }
    }
}