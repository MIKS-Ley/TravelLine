using Zakaz_travel.Models;

namespace Zakaz_travel;

public static class Editor
{
    private static readonly string[] OrderParameters =
    {
        "    - Название товара",
        "    - Количество товара",
        "    - Имя пользователя",
        "    - Адрес доставки",
        "    - Оформить заказ"
    };

    public static void EditOrder( Order order )
    {
        int selectedFieldId = 0;

        while ( true )
        {
            RenderEditOrderScreen( order, selectedFieldId );

            ConsoleKeyInfo key = Console.ReadKey( true );
            switch ( key.Key )
            {
                case ConsoleKey.Escape:
                    return;

                case ConsoleKey.UpArrow when selectedFieldId > 0:
                    selectedFieldId--;
                    break;

                case ConsoleKey.DownArrow when selectedFieldId < OrderParameters.Length - 1:
                    selectedFieldId++;
                    break;

                case ConsoleKey.Enter:
                    HandleFieldSelection( order, selectedFieldId );
                    break;
            }
        }
    }

    public static void StartOrderProcess()
    {
        var order = CollectOrderInformation();
        ConfirmOrder( order );
    }

    private static Order CollectOrderInformation()
    {
        var order = new Order();
        var steps = new[]
        {
            CreateProductNameStep(order),
            CreateQuantityStep(order),
            CreateUserNameStep(order),
            CreateDeliveryAddressStep(order)
        };

        foreach ( var step in steps )
        {
            ProcessOrderStep( order, step );
        }

        return order;
    }

    private static void RenderEditOrderScreen( Order order, int selectedFieldId )
    {
        OrderManagerUI.Zakaz();
        Console.WriteLine( "Редактирование данных заказа:" );
        Console.WriteLine( "Вверх/Вниз - выбор пункта | Enter - выбрать" );
        Console.WriteLine( "--------------------------------------------------" );

        for ( int i = 0; i < OrderParameters.Length; i++ )
        {
            Console.ForegroundColor = i == selectedFieldId ? ConsoleColor.Blue : ConsoleColor.Gray;
            Console.Write( i == selectedFieldId ? "> " : "  " );

            if ( i < OrderParameters.Length - 1 )
            {
                Console.WriteLine( $"{OrderParameters[ i ]}: {GetFieldValue( i, order )}" );
            }
            else
            {
                Console.WriteLine( OrderParameters[ i ] );
            }
            Console.ResetColor();
        }
    }

    private static void HandleFieldSelection( Order order, int fieldId )
    {
        if ( fieldId == OrderParameters.Length - 1 )
        {
            FinalizeOrder( order );
        }
        else
        {
            EditSelectedField( order, fieldId );
        }
    }

    public static void FinalizeOrder( Order order )
    {
        Console.WriteLine( OrderManagerUI.ZakazText );
        OrderManagerUI.ShowConfirmation( order );
        Console.WriteLine( "\nНажмите любую клавишу для завершения..." );
        Console.ReadKey();
        MenuManager.Menu();
    }

    private static void EditSelectedField( Order order, int fieldId )
    {
        string newValue = EditField( fieldId, order );

        switch ( fieldId )
        {
            case 0: order.ProductName = newValue; break;
            case 1 when int.TryParse( newValue, out int quantity ):
                order.Quantity = quantity;
                break;
            case 2: order.UserName = newValue; break;
            case 3: order.DeliveryAddress = newValue; break;
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
            _ => string.Empty
        };
    }

    private static string EditField( int fieldIndex, Order order )
    {
        string currentValue = GetFieldValue( fieldIndex, order );

        while ( true )
        {
            OrderManagerUI.Zakaz();
            Console.WriteLine( $"РЕДАКТИРОВАНИЕ: {OrderParameters[ fieldIndex ].Trim()}" );
            Console.WriteLine( "--------------------------------------------------" );
            Console.WriteLine( $"Текущее значение: {currentValue}" );

            Console.WriteLine( fieldIndex == 1
                ? "Введите новое количество:"
                : "Введите новое значение (или нажмите Enter чтобы оставить текущее):" );

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

    private static OrderStep CreateProductNameStep( Order order ) => new(
        "Введите Название товара:",
        value => order.ProductName = value,
        value => !string.IsNullOrWhiteSpace( value ),
        "Ошибка: Название товара не может быть пустым." );

    private static OrderStep CreateQuantityStep( Order order ) => new(
        "Введите количество товара:",
        value => order.Quantity = int.Parse( value ),
        value => int.TryParse( value, out int qty ) && qty > 0,
        "Ошибка: Введите целое число больше 0!" );

    private static OrderStep CreateUserNameStep( Order order ) => new(
        "Введите Имя пользователя:",
        value => order.UserName = value,
        value => !string.IsNullOrWhiteSpace( value ),
        "Ошибка: Имя пользователя не может быть пустым." );

    private static OrderStep CreateDeliveryAddressStep( Order order ) => new(
        "Введите Адрес доставки:",
        value => order.DeliveryAddress = value,
        value => !string.IsNullOrWhiteSpace( value ),
        "Ошибка: Адрес доставки не может быть пустым." );

    private static void ProcessOrderStep( Order order, OrderStep step )
    {
        bool isValid = false;
        while ( !isValid )
        {
            try
            {
                OrderManagerUI.DisplayOrderScreen( order );
                Console.Write( step.Prompt );

                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if ( !step.Validator( input ) )
                {
                    throw new ArgumentException( step.ErrorMessage );
                }

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
            new MenuOperation("Да", () => { OrderManagerUI.DecorationOrder(order); MenuManager.Menu(); } ),
            new MenuOperation("Нет", () => { EditOrder(order); MenuManager.Menu(); })
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