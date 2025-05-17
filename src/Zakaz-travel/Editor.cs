using Zakaz_travel;
using Zakaz_travel.Models;

public static class Editor
{
    public static Order EditOrder( Order order )
    {
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
            OrderManagerUI.Zakaz();
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
                    return order;

                case ConsoleKey.UpArrow when fieldID > 0:
                    fieldID--;
                    break;

                case ConsoleKey.DownArrow when fieldID < parameters.Length - 1:
                    fieldID++;
                    break;

                case ConsoleKey.Enter:
                    if ( fieldID == parameters.Length - 1 )
                    {
                        Console.WriteLine( OrderManagerUI.ZakazText );
                        OrderManagerUI.ShowConfirmation( order );
                        Console.WriteLine( "\nНажмите любую клавишу для завершения..." );
                        Console.ReadKey();
                    }
                    else
                    {
                        string newValue = EditField( fieldID, parameters, order );

                        switch ( fieldID )
                        {
                            case 0: order.ProductName = newValue; break;
                            case 1:
                                if ( int.TryParse( newValue, out int quantity ) )
                                {
                                    order.Quantity = quantity;
                                }
                                break;
                            case 2: order.UserName = newValue; break;
                            case 3: order.DeliveryAddress = newValue; break;
                        }
                    }
                    break;
            }
        }
    }

    private static string GetFieldValue( int fieldIndex, Order order )
    {
        return fieldIndex switch
        {
            0 => order.ProductName ?? "[не указано]",
            1 => order.Quantity?.ToString() ?? "[не указано]",
            2 => order.UserName ?? "[не указано]",
            3 => order.DeliveryAddress ?? "[не указано]",
            _ => ""
        };
    }

    private static string EditField( int fieldIndex, string[] fields, Order order )
    {
        string currentValue = GetFieldValue( fieldIndex, order );

        while ( true )
        {
            OrderManagerUI.Zakaz();
            Console.WriteLine( $"РЕДАКТИРОВАНИЕ: {fields[ fieldIndex ].Trim()}" );
            Console.WriteLine( "--------------------------------------------------" );
            Console.WriteLine( $"Текущее значение: {currentValue}" );

            if ( fieldIndex == 1 )
            {
                Console.WriteLine( "Введите новое количество:" );
            }
            else
            {
                Console.WriteLine( "Введите новое значение (или нажмите Enter чтобы оставить текущее):" );
            }

            Console.Write( "> " );
            string? newValue = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( newValue ) )
            {
                return currentValue;
            }

            if ( fieldIndex == 1 )
            {
                if ( int.TryParse( newValue, out int quantity ) && quantity > 0 )
                {
                    return quantity.ToString();
                }
                Console.WriteLine( "Ошибка: Введите целое число больше 0!" );
                Console.ReadKey();
            }
            else
            {
                return newValue;
            }
        }
    }

    public static void StartOrderProcess()
    {
        Order order = CollectOrderInformation();
        ConfirmOrder( order );
    }

    private static Order CollectOrderInformation()
    {
        Order order = new Order();
        var steps = new OrderStep[]
        {
            new OrderStep(
                "Введите Название товара:",
                value => order.ProductName = value,
                value => !string.IsNullOrWhiteSpace(value),
                "Ошибка: Название товара не может быть пустым."
            ),
            new OrderStep(
                "Введите количество товара:",
                value => order.Quantity = int.Parse(value),
                value => int.TryParse(value, out int qty) && qty > 0,
                "Ошибка: Введите целое число больше 0!"
            ),
            new OrderStep(
                "Введите Имя пользователя:",
                value => order.UserName = value,
                value => !string.IsNullOrWhiteSpace(value),
                "Ошибка: Имя пользователя не может быть пустым."
            ),
            new OrderStep(
                "Введите Адрес доставки:",
                value => order.DeliveryAddress = value,
                value => !string.IsNullOrWhiteSpace(value),
                "Ошибка: Адрес доставки не может быть пустым."
            )
        };

        foreach ( var step in steps )
        {
            ProcessOrderStep( order, step );
        }

        return order;
    }

    private static void ProcessOrderStep( Order order, OrderStep step )
    {
        bool isValid = false;
        while ( !isValid )
        {
            try
            {
                OrderManagerUI.DisplayOrderScreen( order );
                Console.Write( step.Prompt );

                var input = Console.ReadLine()?.Trim() ?? string.Empty;

                if ( !step.Validator( input ) )
                    throw new ArgumentException( step.ErrorMessage );

                step.ValueSetter( input );
                isValid = true;
            }
            catch ( Exception ex )
            {
                OrderManagerUI.ShowError( ex.Message );
            }
        }
    }

    private static void ConfirmOrder( Order order )
    {
        var options = new[]
        {
            new MenuOperation( "Да", () => OrderManagerUI.FinalizeOrder( order ) ),
            new MenuOperation( "Нет", () => { EditOrder( order ); MenuManager.Menu(); } )
        };

        Action confirmationHeader = () =>
        {
            OrderManagerUI.Zakaz();
            Console.ResetColor();
            Console.WriteLine( $"Здравствуйте, {order.UserName}, вы заказали " +
                            $"{order.Quantity} {order.ProductName} на адрес " +
                            $"{order.DeliveryAddress}, все верно?" );
        };

        MenuManager.CreateMenu(
            operations: options,
            customCodeBeforeRender: confirmationHeader,
            selectedColor: ConsoleColor.Blue
        );
    }
}