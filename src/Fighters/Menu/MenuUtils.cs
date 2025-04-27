namespace Fighters.Menu
{
    public static class MenuUtils
    {
        public static void CreateCustomMenu(
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
}