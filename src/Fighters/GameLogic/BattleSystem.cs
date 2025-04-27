using Fighters.Menu;
using Fighters.Models.Fighters;

namespace Fighters.GameLogic
{
    public static class BattleSystem
    {
        private static Random random = new Random();

        public static void GenerateRoundTable( int roundNumber, List<PlayerConfig> alivePlayers )
        {
            StartMenu.LogoGame();
            Console.WriteLine();
            Console.WriteLine( $"                                                      === РАУНД {roundNumber} ===\n" );

            if ( alivePlayers.Count < 2 )
            {
                Console.WriteLine( "Недостаточно живых бойцов для продолжения!" );
                Console.WriteLine( $"Победитель: {alivePlayers.FirstOrDefault()?.Nickname ?? "нет"}" );
                Console.ReadKey();
                return;
            }

            var pairs = new List<Tuple<PlayerConfig, PlayerConfig>>();
            for ( int i = 0; i < alivePlayers.Count; i += 2 )
            {
                if ( i + 1 < alivePlayers.Count )
                {
                    pairs.Add( new Tuple<PlayerConfig, PlayerConfig>( alivePlayers[ i ], alivePlayers[ i + 1 ] ) );
                }
                else
                {
                    Console.WriteLine( $"[{alivePlayers[ i ].Nickname}] проходит дальше без боя" );
                }
            }
            foreach ( var pair in pairs )
            {
                BattleBetweenPair( pair.Item1, pair.Item2, roundNumber );
            }

            var remainingPlayers = alivePlayers.Where( p => p.Health > 0 ).ToList();

            Console.WriteLine( "\nРаунд завершен!" );
            Console.WriteLine( "Нажмите любую клавишу для продолжения..." );
            Console.ReadKey();

            if ( remainingPlayers.Count > 1 )
            {
                GenerateRoundTable( roundNumber + 1, remainingPlayers );
            }
            else
            {
                StartMenu.LogoWin();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( $"\n Победитель: {remainingPlayers.FirstOrDefault()?.Nickname ?? "нет"}!" );
                Console.WriteLine( "\nНажмите любую клавишу для возврата в меню..." );
                Console.ResetColor();
                Console.ReadKey();
                Program.ShowMainMenu();
            }
        }
        private static void BattleBetweenPair( PlayerConfig player1, PlayerConfig player2, int roundNumber )
        {
            Console.WriteLine( $"\n=== Бой между {player1.Nickname} и {player2.Nickname} ===" );
            Console.WriteLine( "Нажмите любую клавишу для начала боя..." );
            Console.ReadKey();

            while ( player1.Health > 0 && player2.Health > 0 )
            {
                StartMenu.LogoGame();
                Console.WriteLine( $"                                                      === РАУНД {roundNumber} ===" );
                Console.WriteLine( $"{player1.Nickname}: {player1.Health} HP" );
                Console.WriteLine( $"{player2.Nickname}: {player2.Health} HP\n" );
                PlayerConfig attacker, defender;
                if ( random.Next( 2 ) == 0 )
                {
                    attacker = player1;
                    defender = player2;
                }
                else
                {
                    attacker = player2;
                    defender = player1;
                }

                Console.WriteLine( $"Атакует: {attacker.Nickname}" );
                Console.WriteLine( "Нажмите любую клавишу для атаки..." );
                Console.ReadKey();

                int damage = CalculateDamage( attacker, defender );
                defender.Health = Math.Max( 0, defender.Health - damage );

                Console.WriteLine( $"\n{attacker.Nickname} наносит {damage} урона" );
                Console.WriteLine( $"{defender.Nickname} теперь имеет {defender.Health} HP" );

                if ( defender.Health <= 0 )
                {
                    Console.WriteLine( $"\n {defender.Nickname} повержен!" );
                }
                Console.WriteLine( "\nНажмите любую клавишу для продолжения..." );
                Console.ReadKey();
            }
        }
        private static int CalculateDamage( PlayerConfig attacker, PlayerConfig defender )
        {
            int baseDamage = attacker.WeaponDamage + attacker.Strength / 2;
            double variation = 1.0 + ( random.NextDouble() * 0.3 - 0.2 );
            int damage = ( int )( baseDamage * variation );

            if ( random.Next( 100 ) < 10 )
            {
                damage *= 2;
                Console.Write( "Критический удар! " );
            }

            int armorReduction = defender.ArmorDurability / 2;
            return Math.Max( 1, damage - armorReduction );
        }
    }
}