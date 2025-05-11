using Fighters.Menu;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameLogic
{

    public static class PlayerCreation
    {
        private static Random random = new Random();
        public static int ReadPlayerCount()
        {
            while ( true )
            {
                StartMenu.ShowGameLogo();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write( "Введите количество игроков: " );
                Console.ResetColor();
                if ( int.TryParse( Console.ReadLine(), out int count ) && count > 0 )
                    return count;
                Console.WriteLine( "Некорректный ввод! Нажмите любую клавишу..." );
                Console.ReadKey();
            }
        }
        public static List<PlayerConfig> CreatePlayers( int count )
        {
            var players = new List<PlayerConfig>();
            for ( int i = 0; i < count; i++ )
            {
                StartMenu.ShowGameLogo();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( $"\nСоздание игрока {i + 1} из {count}" );
                Console.ResetColor();
                Console.WriteLine( "Нажмите любую клавишу для продолжения..." );
                Console.ReadKey();

                var player = new PlayerConfig { Id = i + 1 };
                SelectRace( player );
                EnterNickname( player );
                SelectArmor( player );
                SelectWeapon( player );
                CalculateStats( player );
                players.Add( player );

                StartMenu.ShowGameLogo();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( $"\nИгрок {player.Nickname} успешно создан!" );
                Console.ResetColor();
                Console.WriteLine( "Нажмите любую клавишу для продолжения..." );
                Console.ReadKey();
            }
            return players;
        }

        public static void SelectRace( PlayerConfig player )
        {
            // Создаем экземпляры всех рас
            var races = new Dictionary<string, IRace>
            {
                { "Рыцарь", new Knight() },
                { "Маг", new Mag() },
                { "Гоблин", new Goblin() },
                { "Викинг", new Viking() },
                { "Эльф", new Elf() }
            };

            string[] raceNames = races.Keys.ToArray();
            string[] descriptions = raceNames.Select( name =>
                $"Сила: {races[ name ].Damage} | Здоровье: {races[ name ].Health} | Броня: {races[ name ].Armor}" ).ToArray();

            // Цвета для каждой расы
            var raceColors = new Dictionary<string, ConsoleColor>
            {
                { "Рыцарь", ConsoleColor.White },
                { "Маг", ConsoleColor.Blue },
                { "Гоблин", ConsoleColor.Green },
                { "Викинг", ConsoleColor.Red },
                { "Эльф", ConsoleColor.Magenta }
            };

            int selectedIndex = 0;
            while ( true )
            {
                StartMenu.ShowGameLogo();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "Выберите расу:\n" );
                Console.ResetColor();
                for ( int i = 0; i < raceNames.Length; i++ )
                {
                    if ( i == selectedIndex )
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write( "> " );
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write( "  " );
                    }
                    Console.WriteLine( $"{raceNames[ i ]} - {descriptions[ i ]}" );
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "\nВнешний вид:" );
                Console.ResetColor();
                var currentRace = races[ raceNames[ selectedIndex ] ];

                // Устанавливаю цвет для текущей расы
                if ( raceColors.TryGetValue( raceNames[ selectedIndex ], out var color ) )
                {
                    Console.ForegroundColor = color;
                }

                Console.WriteLine( currentRace.Face );
                Console.ResetColor();

                // Обработка ввода пользователя
                var key = Console.ReadKey( true ).Key;
                if ( key == ConsoleKey.UpArrow ) selectedIndex = Math.Max( 0, selectedIndex - 1 );
                else if ( key == ConsoleKey.DownArrow ) selectedIndex = Math.Min( raceNames.Length - 1, selectedIndex + 1 );
                else if ( key == ConsoleKey.Enter )
                {
                    player.Race = raceNames[ selectedIndex ];
                    return;
                }
                else if ( key == ConsoleKey.Escape ) return;
            }
        }
        public static void EnterNickname( PlayerConfig player )
        {
            while ( true )
            {
                StartMenu.ShowGameLogo();
                Console.WriteLine();
                Console.WriteLine( $"Выбранная раса: {player.Race}" );
                Console.WriteLine( $"Текущий ник: {( string.IsNullOrEmpty( player.Nickname ) ? "не установлен" : player.Nickname )}" );
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write( "\nВведите никнейм для Игрока {0}: ", player.Id );
                Console.ResetColor();
                string? input = Console.ReadLine()?.Trim();

                if ( !string.IsNullOrWhiteSpace( input ) )
                {
                    player.Nickname = input;
                    return;
                }
                Console.WriteLine( "Никнейм не может быть пустым! Нажмите любую клавишу..." );
                Console.ReadKey();
            }
        }

        public static void SelectArmor( PlayerConfig player )
        {
            var races = new Dictionary<string, IRace>
            {
                { "Рыцарь", new Knight() },
                { "Маг", new Mag() },
                { "Гоблин", new Goblin() },
                { "Викинг", new Viking() },
                { "Эльф", new Elf() }
            };

            var armors = new Dictionary<string, IArmor>
            {
                {"Без брони", new NoArmor()},
                {"Кожаная", new LeatherArmor()},
                {"Железная", new IronArmor()},
                {"Золотая", new GoldenArmor()},
                {"Алмазная", new DiamondArmor()}
            };

            string[] armorNames = armors.Keys.ToArray();
            string[] armorDescriptions = armorNames.Select( name =>
                name == "Без брони" ? "Нет защиты" : $"Прочность: {armors[ name ].Armor}" ).ToArray();

            int selectedIndex = 0;
            while ( true )
            {
                StartMenu.ShowGameLogo();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "Выберите броню:\n" );
                Console.ResetColor();
                // Отображаем меню выбора с зеленой стрелкой
                for ( int i = 0; i < armorNames.Length; i++ )
                {
                    if ( i == selectedIndex )
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write( "> " );
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write( "  " );
                    }
                    Console.WriteLine( $"{armorNames[ i ]} - {armorDescriptions[ i ]}" );
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "\nВыбранная броня:" );
                Console.ResetColor();
                var currentArmor = armors[ armorNames[ selectedIndex ] ];
                if ( races.TryGetValue( player.Race, out var currentRace ) )
                {
                    Console.WriteLine( armorDescriptions[ selectedIndex ] );
                    Console.WriteLine( currentRace.Face );

                    if ( currentArmor.Color.HasValue )
                    {
                        Console.ForegroundColor = currentArmor.Color.Value;
                    }
                    Console.Write( currentRace.Body );
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine( "Неизвестная раса." );
                }

                var key = Console.ReadKey( true ).Key;
                if ( key == ConsoleKey.UpArrow ) selectedIndex = Math.Max( 0, selectedIndex - 1 );
                else if ( key == ConsoleKey.DownArrow ) selectedIndex = Math.Min( armorNames.Length - 1, selectedIndex + 1 );
                else if ( key == ConsoleKey.Enter )
                {
                    player.Armor = armorNames[ selectedIndex ];
                    player.ArmorDurability = currentArmor.Armor;
                    return;
                }
                else if ( key == ConsoleKey.Escape ) return;
            }
        }

        public static void SelectWeapon( PlayerConfig player )
        {
            // Создаем словарь с оружием, используя классы оружия
            var weapons = new Dictionary<string, IWeapon>
            {
                {"Без оружия", new Firsts() },
                {"Меч", new Sword()},
                {"Кинжал", new Dagger() },
                {"Копье", new Spear() },
                {"Лук", new Bow()},
                {"Посох", new Staff()},
                {"Дробовик", new Shotgun()},
                {"Топор", new Axe()}
            };

            string[] weaponNames = weapons.Keys.ToArray();
            int selectedIndex = 0;

            while ( true )
            {
                StartMenu.ShowGameLogo();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "   Выберите оружие:" );
                Console.ResetColor();

                // Отображаем меню выбора с зелеными стрелками
                for ( int i = 0; i < weaponNames.Length; i++ )
                {
                    if ( i == selectedIndex )
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write( "> " );
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write( "  " );
                    }
                    Console.WriteLine( $"{weaponNames[ i ]} (Урон: {weapons[ weaponNames[ i ] ].Damage})" );
                }

                // Обработка ввода пользователя
                var key = Console.ReadKey( true ).Key;
                if ( key == ConsoleKey.UpArrow )
                {
                    selectedIndex = Math.Max( 0, selectedIndex - 1 );
                }
                else if ( key == ConsoleKey.DownArrow )
                {
                    selectedIndex = Math.Min( weaponNames.Length - 1, selectedIndex + 1 );
                }
                else if ( key == ConsoleKey.Enter )
                {
                    player.Weapon = weaponNames[ selectedIndex ];
                    player.WeaponDamage = weapons[ weaponNames[ selectedIndex ] ].Damage;
                    return;
                }
                else if ( key == ConsoleKey.Escape )
                {
                    return;
                }
            }
        }

        public static void CalculateStats( PlayerConfig player )
        {
            player.Health = player.Race switch
            {
                "Рыцарь" => 100,
                "Маг" => 80,
                "Гоблин" => 90,
                "Викинг" => 120,
                _ => 100
            };

            player.Strength = player.Race switch
            {
                "Рыцарь" => 15,
                "Маг" => 10,
                "Гоблин" => 12,
                "Викинг" => 18,
                _ => 10
            };
        }
    }
}
