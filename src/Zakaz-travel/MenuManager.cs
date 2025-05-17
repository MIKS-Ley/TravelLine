using Zakaz_travel.Menu;

namespace Zakaz_travel.Models
{
    public class MenuManager
    {
        public static void CreateMenu(
            MenuOperation[] operations,
            string? title = null,
            ConsoleColor selectedColor = ConsoleColor.Green,
            ConsoleColor defaultColor = ConsoleColor.Gray,
            bool loopMenu = true,
            string pointer = "> ",
            string unselectedPointer = "  ",
            Action? customCodeBeforeRender = null )
        {
            if ( operations == null || operations.Length == 0 )
                throw new ArgumentException( "Операции меню не могут быть пустыми" );

            int selectedIndex = 0;
            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                customCodeBeforeRender?.Invoke();

                // Отображение заголовка
                if ( !string.IsNullOrEmpty( title ) )
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( title + "\n" );
                    Console.ForegroundColor = defaultColor;
                }

                // Отображение пунктов меню
                for ( int i = 0; i < operations.Length; i++ )
                {
                    Console.Write( i == selectedIndex ? pointer : unselectedPointer );
                    Console.ForegroundColor = i == selectedIndex ? selectedColor : defaultColor;
                    Console.WriteLine( operations[ i ].Text );
                    Console.ResetColor();
                }

                // Обработка ввода
                switch ( Console.ReadKey( true ).Key )
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = ( selectedIndex - 1 + operations.Length ) % operations.Length;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = ( selectedIndex + 1 ) % operations.Length;
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        operations[ selectedIndex ].Action.Invoke();
                        if ( !loopMenu ) return;
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            } while ( true );
        }

        public static void Menu()
        {
            var menumanager = new[]
            {
                new MenuOperation("Начать", Editor.StartOrderProcess),
                new MenuOperation("Помощь", HelpMenu.DisplayHelpMenu),
                new MenuOperation("Выход", () => Environment.Exit(0))
            };

            Action header = () =>
            {
                LogoOrderManager.Zakaz();
                Console.Title = "Zakaz-travel.com";
                Console.WriteLine( "Выполнил: Клыков Михаил." );
                Console.WriteLine( "Выберите действие:" );
            };

            CreateMenu(
                operations: menumanager,
                customCodeBeforeRender: header,
                selectedColor: ConsoleColor.Blue
            );
        }
    }

}
