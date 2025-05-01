using Fighters.Menu;

namespace Zakaz_travel.Models
{
    public class Zakaz_travelMenu
    {
        public static void Menu()
        {
            Console.ResetColor();
            string[] options = { "Начать", "Помощь", "Выход" };
            Action[] actions =
            {
                StartWebsite,
                () => { Editor.DisplayHelpMenu(); Menu(); },
                () => Environment.Exit(0)
            };

            Action customCode = () =>
            {
                Editor.Zakaz();
                Console.Title = "Zakaz-travel.com";
                Console.WriteLine( "Выполнил: Клыков Михаил." );
                Console.WriteLine( "Выберите действие:" );
            };

            MenuConstructor.CreateCustomMenu
            (
                menuItems: options,
                actions: actions,
                selectedColor: ConsoleColor.Blue,
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
                    order.DisplayToConsole();
                },
                errorMessage: "Ошибка: Название товара не может быть пустым."
            );

            order.Quantity = Editor.ReadPositiveIntInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole();
                },
                errorMessage: "Ошибка: Введите целое число больше 0!"
            );

            order.UserName = Editor.ReadNonEmptyInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole();
                },
                errorMessage: "Ошибка: Имя пользователя не может быть пустым."
            );

            order.DeliveryAddress = Editor.ReadNonEmptyInput
            (
                displayAction: () =>
                {
                    Editor.Zakaz();
                    order.DisplayToConsole();
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

            MenuConstructor.CreateCustomMenu(
                menuItems: options,
                actions: actions,
                customCodeBeforeRender: customCode,
                selectedColor: ConsoleColor.Blue
            );
        }
    }
}
