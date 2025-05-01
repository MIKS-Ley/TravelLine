namespace Zakaz_travel.Models
{
    public class Order
    {
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public string? UserName { get; set; }
        public string? DeliveryAddress { get; set; }

        public void DisplayToConsole()
        {
            Decoration();

            Console.ForegroundColor = ConsoleColor.Blue;

            if ( !string.IsNullOrWhiteSpace( ProductName ) )
            {
                Console.WriteLine( $"- Название товара: {ProductName}" );
            }
            if ( Quantity.HasValue )
            {
                Console.WriteLine( $"- Количество товара: {Quantity}" );
            }
            if ( !string.IsNullOrWhiteSpace( UserName ) )
            {
                Console.WriteLine( $"- Имя пользователя: {UserName}" );
            }
            if ( !string.IsNullOrWhiteSpace( DeliveryAddress ) )
            {
                Console.WriteLine( $"- Адрес доставки: {DeliveryAddress}" );
            }

            Console.ResetColor();
        }

        private void Decoration()
        {
            Editor.Zakaz();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine( "\n                                             Оформление заказа" );
            Console.WriteLine( "                                           ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄" );
            Console.ResetColor();
        }
    }
}
