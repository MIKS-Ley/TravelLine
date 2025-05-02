namespace Zakaz_travel.Models
{
    public class ZakazTravelMenu
    {
        public static void Menu()
        {
            var menuOperations = new[]
            {
                new MenuOperation("Начать", StartOrderProcess),
                new MenuOperation("Помощь", Editor.DisplayHelpMenu),
                new MenuOperation("Выход", () => Environment.Exit(0))
            };

            Action header = () =>
            {
                Editor.Zakaz();
                Console.Title = "Zakaz-travel.com";
                Console.WriteLine( "Выполнил: Клыков Михаил." );
                Console.WriteLine( "Выберите действие:" );
            };

            MenuConstructor.CreateMenu(
                operations: menuOperations,
                customCodeBeforeRender: header,
                selectedColor: ConsoleColor.Blue
            );
        }

        private static void StartOrderProcess()
        {
            var order = CollectOrderInformation();
            ConfirmOrder( order );
        }

        private static Order CollectOrderInformation()
        {
            var order = new Order();
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
                    DisplayOrderScreen( order );
                    Console.Write( step.Prompt );

                    var input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if ( !step.Validate( input ) )
                        throw new ArgumentException( step.ErrorMessage );

                    step.SetValue( input );
                    isValid = true;
                }
                catch ( Exception ex )
                {
                    ShowError( ex.Message );
                }
            }
        }

        private static void ConfirmOrder( Order order )
        {
            var options = new[]
            {
                new MenuOperation("Да", () => FinalizeOrder(order)),
                new MenuOperation("Нет", () => EditOrder(order))
            };

            Action confirmationHeader = () =>
            {
                Editor.Zakaz();
                Console.ResetColor();
                Console.WriteLine( $"Здравствуйте, {order.UserName}, вы заказали " +
                                $"{order.Quantity} {order.ProductName} на адрес " +
                                $"{order.DeliveryAddress}, все верно?" );
            };

            MenuConstructor.CreateMenu(
                operations: options,
                customCodeBeforeRender: confirmationHeader,
                selectedColor: ConsoleColor.Blue
            );
        }

        private static void FinalizeOrder( Order order )
        {
            Editor.ShowConfirmation( order );
            Console.ReadKey();
            Menu();
        }

        private static void EditOrder( Order order )
        {
            Editor.EditOrder( order );
            Menu();
        }

        private static void DisplayOrderScreen( Order order )
        {
            Console.Clear();
            Editor.Zakaz();
            order.DisplayToConsole();
        }

        private static void ShowError( string message )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine( message );
            Console.ResetColor();
            Console.WriteLine( "Нажмите любую клавишу для продолжения..." );
            Console.ReadKey();
        }
    }

    public record OrderStep(
        string Prompt,
        Action<string> SetValue,
        Func<string, bool> Validate,
        string ErrorMessage
    );
}