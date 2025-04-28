using CarFactory.Models;
using CarFactory.Models.Enums;
using Fighters.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactory.Menu
{
    public static class CarMenu
    {
        private static readonly List<Car> cars = new();
        private static string currentModel = string.Empty;
        private static string currentBrand = string.Empty;

        public static void ShowMainMenu()
        {
            var options = new[]
            {
                "Создать новое авто",
                "Показать список созданных автомобилей",
                "Выход"
            };

            var actions = new Action[]
            {
                AddCar,
                ListOfCar,
                Program.ExitGame
            };

            MenuConstructor.CreateCustomMenu(
                menuItems: options,
                actions: actions,
                selectedColor: ConsoleColor.Blue,
                customCodeBeforeRender: () =>
                {
                    Logo.MainLogo();
                    Console.WriteLine("\nВыполнил: Клыков Михаил.");
                }
            );
        }

        private static void AddCar()
        {
            currentModel = GetModelName();
            if (currentModel == "-") return;

            currentBrand = SelectBrand();
            if (currentBrand == "Выход") return;

            var config = ConfigureCar();
            if (config == null) return;

            var car = CarFactory.CreateCar(currentModel, currentBrand, config);
            cars.Add(car);

            ShowSuccessMessage($"Автомобиль {car.Brand} {car.Model} успешно создан!\n{car}");
        }

        private static string GetModelName()
        {
            while (true)
            {
                Console.Clear();
                Logo.MainLogo();
                Console.WriteLine("\nДля выхода введите '-'");
                Console.Write("Название модели: ");

                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    ShowErrorMessage("Название не может быть пустым!");
                    continue;
                }

                return input.Trim();
            }
        }

        private static string SelectBrand()
        {
            var brands = new[]
            {
                "Audi", "BMW", "Lada",
                "Mercedes", "Toyota", "Выход"
            };

            string selectedBrand = string.Empty;

            MenuConstructor.CreateCustomMenu(
                menuItems: brands,
                actions: brands.Select(b => (Action)(() => selectedBrand = b)).ToArray(),
                selectedColor: ConsoleColor.Blue,
                customCodeBeforeRender: () =>
                {
                    Logo.MainLogo();
                    Console.WriteLine("\nВыберите марку авто");
                },
                loopMenu: false
            );

            return selectedBrand;
        }

        private static CarConfiguration ConfigureCar()
        {
            var config = new CarConfiguration();
            bool configurationComplete = false;

            while (!configurationComplete)
            {
                var options = new[]
                {
                    $"Двигатель: {config.Engine}",
                    $"Коробка передач: {config.Transmission}",
                    $"Цвет: {config.Color}",
                    $"Руль: {config.SteeringWheel}",
                    $"Кузов: {config.Body}",
                    "Готово",
                    "Отмена"
                };

                MenuConstructor.CreateCustomMenu(
                    menuItems: options,
                    actions: new Action[]
                    {
                        () => SelectEngine(config),
                        () => SelectTransmission(config),
                        () => SelectColor(config),
                        () => SelectSteeringWheel(config),
                        () => SelectBodyType(config),
                        () => configurationComplete = true,
                        () => { config = null; configurationComplete = true; }
                    },
                    selectedColor: ConsoleColor.Blue,
                    customCodeBeforeRender: () =>
                    {
                        Logo.MainLogo();
                        Console.WriteLine($"\nКонфигурация: {currentBrand} {currentModel}");
                        Console.WriteLine($"Характеристики: {config.MaxSpeed} км/ч, {config.Gears} передач");
                    },
                    loopMenu: false
                );
            }

            return config;
        }

        private static void SelectEngine(CarConfiguration config)
        {
            ShowEnumSelectionMenu(
                title: "Выберите двигатель:",
                enumValues: Enum.GetValues<EngineType>(),
                onSelect: value => config.Engine = value
            );
        }

        private static void SelectTransmission(CarConfiguration config)
        {
            ShowEnumSelectionMenu(
                title: "Выберите коробку передач:",
                enumValues: Enum.GetValues<TransmissionType>(),
                onSelect: value => config.Transmission = value
            );
        }

        private static void SelectColor(CarConfiguration config)
        {
            ShowEnumSelectionMenu(
                title: "Выберите цвет:",
                enumValues: Enum.GetValues<Color>(),
                onSelect: value => config.Color = value
            );
        }

        private static void SelectSteeringWheel(CarConfiguration config)
        {
            ShowEnumSelectionMenu(
                title: "Выберите позицию руля:",
                enumValues: Enum.GetValues<SteeringWheelPosition>(),
                onSelect: value => config.SteeringWheel = value
            );
        }

        private static void SelectBodyType(CarConfiguration config)
        {
            ShowEnumSelectionMenu(
                title: "Выберите тип кузова:",
                enumValues: Enum.GetValues<BodyType>(),
                onSelect: value => config.Body = value
            );
        }

        private static void ShowEnumSelectionMenu<T>(string title, T[] enumValues, Action<T> onSelect)
            where T : Enum
        {
            var options = enumValues.Select(e => e.ToString())
                                   .Append("Назад")
                                   .ToArray();

            var actions = enumValues.Select<T, Action>(value => () => onSelect(value))
                                  .Append(() => { })
                                  .ToArray();

            MenuConstructor.CreateCustomMenu(
                menuItems: options,
                actions: actions,
                selectedColor: ConsoleColor.Blue,
                title: title,
                loopMenu: false
            );
        }

        public static void ListOfCar()
        {
            if (!cars.Any())
            {
                ShowInfoMessage("Нет созданных автомобилей.");
                return;
            }

            int index = 0;
            while (true)
            {
                Console.Clear();
                Logo.MainLogo();
                Console.WriteLine("\nСписок автомобилей:");

                for (int i = 0; i < cars.Count; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("> ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    Console.WriteLine($"{cars[i].Brand} {cars[i].Model}");
                }

                Console.WriteLine("\nСтрелки: навигация | Enter: детали | Esc: выход");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        index = (index - 1 + cars.Count) % cars.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        index = (index + 1) % cars.Count;
                        break;
                    case ConsoleKey.Enter:
                        ShowCarDetails(cars[index]);
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private static void ShowCarDetails(Car car)
        {
            Console.Clear();
            Console.WriteLine(car);
            Console.WriteLine($"\nДата производства: {car.ProductionDate:dd.MM.yyyy}");
            Console.WriteLine($"VIN: {car.VIN}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void ShowSuccessMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void ShowInfoMessage(string message)
        {
            Console.Clear();
            Logo.MainLogo();
            Console.WriteLine($"\n{message}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nОшибка: {message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}