using Zakaz_travel.Models;

public static class Editor
{
    private const string ZakazText = @"
 /$$$$$$$$           /$$                                     /$$                                              /$$
|_____ $$           | $$                                    | $$                                             | $$
     /$$/   /$$$$$$ | $$   /$$  /$$$$$$  /$$$$$$$$         /$$$$$$    /$$$$$$   /$$$$$$  /$$    /$$  /$$$$$$ | $$
    /$$/   |____  $$| $$  /$$/ |____  $$|____ /$$/ /$$$$$$|_  $$_/   /$$__  $$ /$$__  $$|  $$  /$$/ |____  $$| $$
   /$$/     /$$$$$$$| $$$$$$/   /$$$$$$$   /$$$$/ |______/  | $$    | $$  \__/| $$$$$$$$ \  $$/$$/   /$$$$$$$| $$
  /$$/     /$$__  $$| $$_  $$  /$$__  $$  /$$__/            | $$ /$$| $$      | $$_____/  \  $$$/   /$$__  $$| $$
 /$$$$$$$$|  $$$$$$$| $$ \  $$|  $$$$$$$ /$$$$$$$$          |  $$$$/| $$      |  $$$$$$$   \  $/   |  $$$$$$$| $$
|________/ \_______/|__/  \__/ \_______/|________/           \___/  |__/       \_______/    \_/     \_______/|__/";
    public static void Zakaz()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine( ZakazText );
        Console.ResetColor();
    }

    public static void EditOrder( Order order )
    {
        // Изменил название переменной с parameter на parameters для ясности
        string[] parameters = {
            "    - Название товара",
            "    - Количество товара",
            "    - Имя пользователя",
            "    - Адрес доставки",
            "    - Оформить заказ"
        };

        int fieldID = 0;

        while ( true )
        {
            Zakaz();
            Console.WriteLine( "Редактирование данных заказа:" );
            Console.WriteLine( "Вверх/Вниз - выбор пункта | Enter - выбрать" );
            Console.WriteLine( "--------------------------------------------------" );

            for ( int i = 0; i < parameters.Length; i++ )
            {
                if ( i == fieldID )
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write( "> " );
                }
                else
                {
                    Console.Write( "  " );
                }

                if ( i < parameters.Length - 1 )
                {
                    // Заменил Value() на GetFieldValue() для лучшей читаемости
                    string meaning = GetFieldValue( i, order );
                    Console.WriteLine( $"{parameters[ i ]}: {meaning}" );
                }
                else
                {
                    Console.WriteLine( parameters[ i ] );
                }
                Console.ResetColor();
            }

            var key = Console.ReadKey( true );

            switch ( key.Key )
            {
                case ConsoleKey.Escape:
                    return;

                case ConsoleKey.UpArrow when fieldID > 0:
                    fieldID--;
                    break;

                case ConsoleKey.DownArrow when fieldID < parameters.Length - 1:
                    fieldID++;
                    break;

                case ConsoleKey.Enter:
                    if ( fieldID == parameters.Length - 1 )
                    {
                        Console.Clear();
                        // Переименовал TheEnd() в ShowConfirmation() для ясности
                        ShowConfirmation( order );
                        Console.WriteLine( "\nНажмите любую клавишу для завершения..." );
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        string newValue = EditField( fieldID, parameters, order );

                        switch ( fieldID )
                        {
                            case 0: order.ProductName = newValue; break;
                            case 1: // Поменял
                                if ( int.TryParse( newValue, out int quantity ) )
                                {
                                    order.Quantity = quantity;
                                }
                            case 2: order.UserName = newValue; break;
                            case 3: order.DeliveryAddress = newValue; break;
                        }
                    }
                    break;
            }
        }
    }
    private static void ShowConfirmation( Order order )
    {
        Zakaz();
        DateTime todayDate = DateTime.Today;
        DateTime futureDate = todayDate.AddDays( 3 );
        Console.WriteLine( $"{order.UserName}! Ваш заказ {order.ProductName} в количестве {order.Quantity} оформлен!" +
            $"\nОжидайте доставку по адресу {order.DeliveryAddress} к {futureDate:dd.MM.yyyy}. Ваш Zakaz-travel.com!" );
    }

    // Переименовал  в GetFieldValue()
    private static string GetFieldValue( int fieldIndex, Order order )
    {
        return fieldIndex switch
        {
            0 => order.ProductName,
            1 => order.Quantity,
            2 => order.UserName,
            3 => order.DeliveryAddress,
            _ => ""
        };
    }

    private static string EditField( int fieldIndex, string[] fields, Order order )
    {
        string currentValue = GetFieldValue( fieldIndex, order );

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
            if ( !int.TryParse( newValue, out int parsedQty ) || parsedQty < 0 )
            {
                while ( true )
                {
                    Console.Clear();
                    Zakaz();
                    Console.WriteLine( $"РЕДАКТИРОВАНИЕ: {fields[ fieldIndex ].Trim()}" );
                    Console.WriteLine( "--------------------------------------------------" );
                    Console.WriteLine( $"Текущее значение: {currentValue}" );
                    Console.WriteLine( "Неверный ввод. Пожалуйста, введите целое положительное число:" );
                    Console.Write( "> " );
                    string? QtyInput = Console.ReadLine();

                    if ( string.IsNullOrWhiteSpace( QtyInput ) )
                    {
                        return currentValue;
                    }

                    if ( int.TryParse( QtyInput, out int Qty ) && Qty >= 0 )
                    {
                        return Qty.ToString();
                    }
                }
            }
            return parsedQty.ToString();
        }
        return string.IsNullOrWhiteSpace( newValue ) ? currentValue : newValue;
    }
}