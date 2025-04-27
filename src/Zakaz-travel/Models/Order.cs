namespace Zakaz_travel.Models
{
    public class Order
    {
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public string? UserName { get; set; }
        public string? DeliveryAddress { get; set; }

        public void DisplayToConsole( int step = 0 )
        {
            Decoration();

            switch ( step )
            {
                case 0: // Ввод названия товара
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write( "- Название товара: " );
                    break;

                case 1: // Ввод количества
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine( $"- Название товара: {ProductName}" );
                    Console.Write( "- Количество товара: " );
                    break;

                case 2: // Ввод имени
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine( $"- Название товара: {ProductName}" );
                    Console.WriteLine( $"- Количество товара: {Quantity}" );
                    Console.Write( "- Имя пользователя: " );
                    break;

                case 3: // Ввод адреса
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine( $"- Название товара: {ProductName}" );
                    Console.WriteLine( $"- Количество товара: {Quantity}" );
                    Console.WriteLine( $"- Имя пользователя: {UserName}" );
                    Console.Write( "- Адрес доставки: " );
                    break;

                default: // Просмотр всех данных
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine( $"- Название товара: {ProductName}" );
                    Console.WriteLine( $"- Количество товара: {Quantity}" );
                    Console.WriteLine( $"- Имя пользователя: {UserName}" );
                    Console.WriteLine( $"- Адрес доставки: {DeliveryAddress}" );
                    break;
            }

            Console.ResetColor();
        }

        private void Decoration()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine( Editor.ZakazText );
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine( "\n                                             Оформление заказа" );
            Console.WriteLine( "                                           ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄" );
            Console.ResetColor();
        }
    }
}