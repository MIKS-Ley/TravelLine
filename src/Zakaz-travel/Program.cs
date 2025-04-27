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
            ()=>{Editor.DisplayHelpMenu(); Menu(); },
            () => { Environment.Exit(0); }
        };

        Action customCode = () =>
        {
            Editor.Zakaz();
            Console.Title = "Zakaz-travel.com";
            Console.WriteLine( "Выполнил: Клыков Михаил." );
            Console.WriteLine( "Выберите действие:" );
        };

        CreateCustomMenu(
            menuItems: options,
            actions: actions,
            selectedColor: ConsoleColor.Blue,
            title: null,
            customCodeBeforeRender: customCode
        );
    }
    static void StartWebsite()
    {
        var order = new Order();

        order.ProductName = Editor.ReadNonEmptyInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole( 0 );
                },
                errorMessage: "Ошибка: Название товара не может быть пустым."
            );
        order.Quantity = Editor.ReadPositiveIntInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole( 1 );
                },
                errorMessage: "Ошибка: Введите целое число больше 0!"
            );
        order.UserName = Editor.ReadNonEmptyInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole( 2 );
                },
                errorMessage: "Ошибка: Имя пользователя не может быть пустым."
            );
        order.DeliveryAddress = Editor.ReadNonEmptyInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole( 3 );
                },
                errorMessage: "Ошибка: Адрес доставки не может быть пустым."
            );
        string[] options = { "Да", "Нет" };
        Action[] actions =
        {
            () =>
            {
                Editor.ShowConfirmation(order);
                Console.ReadKey();
                Menu();
            },
            () =>
            {
                var updatedOrder = Editor.EditOrder(order);
                Menu();
            }
        };
        Action customCode = () =>
        {
            Console.ResetColor();
            Console.WriteLine( $"Здравствуйте, {order.UserName}, вы заказали {order.Quantity} {order.ProductName} " +
                             $"на адрес {order.DeliveryAddress}, все верно?" );
        };

        CreateCustomMenu
            (
                menuItems: options,
                actions: actions,
                customCodeBeforeRender: customCode,
                selectedColor: ConsoleColor.Blue
            );
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

            customCodeBeforeRender?.Invoke();

            if ( !string.IsNullOrEmpty( title ) )
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine( title + "\n" );
                Console.ForegroundColor = defaultColor;
            }
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
