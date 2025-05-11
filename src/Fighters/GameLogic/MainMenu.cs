using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fighters.Menu;
using Fighters.Models.Fighters;

namespace Fighters.GameLogic
{
    public class MainMenu
    {
        private static List<PlayerConfig> players = new List<PlayerConfig>();

        public static void ShowMainMenu()
        {
            Console.ResetColor();
            string[] options = { "Начать игру", "Выход" };
            Action[] actions = { StartGame, ExitGame };

            Action customCode = () =>
            {
                StartMenu.ShowGameLogo();
                Console.Title = "Fighters";
                Console.WriteLine();
                Console.WriteLine( "Выполнил: Клыков Михаил." );
            };

            MenuUtils.CreateCustomMenu
            (
                menuItems: options,
                actions: actions,
                selectedColor: ConsoleColor.Green,
                customCodeBeforeRender: customCode
            );
        }

        public static void StartGame()
        {
            int playerCount = PlayerCreation.ReadPlayerCount();
            players = PlayerCreation.CreatePlayers( playerCount );
            ShowSummaryTable();
        }
        public static void ShowSummaryTable()
        {
            StartMenu.ShowGameLogo();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine( "                                                      Таблица играков" );
            Console.WriteLine();
            Console.ResetColor();
            var alivePlayers = players.Where( p => p.Health > 0 ).ToList();

            Console.WriteLine( "| Номер | Раса     | Имя      | Броня          | Оружие         | Здоровье | Сила | Прочность брони | Урон оружия |" );
            Console.WriteLine( new string( '-', 100 ) );

            foreach ( var player in alivePlayers )
            {
                Console.WriteLine( $"| {player.Id,-6} | {player.Race,-8} | {player.Nickname,-8} | " +
                $"{player.Armor,-14} | {player.Weapon,-14} | {player.Health,-8} | " +
                $"{player.Strength,-4} | {player.ArmorDurability,-15} | {player.WeaponDamage,-11} |" );
            }

            int deadCount = players.Count - alivePlayers.Count;
            if ( deadCount > 0 )
            {
                Console.WriteLine( $"\nУдалено {deadCount} погибших бойцов из таблицы" );
            }

            Console.WriteLine( "\nНажмите любую клавишу для продолжения..." );
            Console.ReadKey();
            BattleSystem.GenerateRoundTable( 1, alivePlayers );
        }

        public static void ExitGame()
        {
            Environment.Exit( 0 );
        }
    }
}
