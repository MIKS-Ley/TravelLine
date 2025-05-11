using Fighters.Menu;
using Fighters.Models.Fighters;

namespace Fighters.GameLogic
{
    public static class BattleSystem
    {
        public static void GenerateRoundTable( int roundNumber, IReadOnlyList<IFighter> aliveFighters )
        {
            StartMenu.ShowGameLogo();
            Console.WriteLine();
            Console.WriteLine( $"                                                      === РАУНД {roundNumber} ===\n" );

            if ( aliveFighters.Count < 2 )
            {
                Console.WriteLine( "Недостаточно живых бойцов для продолжения!" );
                Console.WriteLine( $"Победитель: {aliveFighters.FirstOrDefault()?.Nickname ?? "нет"}" );
                Console.ReadKey();
                return;
            }

            var pairs = new List<Tuple<IFighter, IFighter>>();
            for ( int i = 0; i < aliveFighters.Count; i += 2 )
            {
                if ( i + 1 < aliveFighters.Count )
                {
                    pairs.Add( new Tuple<IFighter, IFighter>( aliveFighters[ i ], aliveFighters[ i + 1 ] ) );
                }
                else
                {
                    Console.WriteLine( $"[{aliveFighters[ i ].Nickname}] проходит дальше без боя" );
                }
            }

            foreach ( var pair in pairs )
            {
                BattleBetweenPair( pair.Item1, pair.Item2, roundNumber );
            }

            var remainingFighters = aliveFighters.Where( f => f.Health > 0 ).ToList();

            Console.WriteLine( "\nРаунд завершен!" );
            Console.WriteLine( "Нажмите любую клавишу для продолжения..." );
            Console.ReadKey();

            if ( remainingFighters.Count > 1 )
            {
                GenerateRoundTable( roundNumber + 1, remainingFighters );
            }
            else
            {
                StartMenu.LogoWin();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( $"\n Победитель: {remainingFighters.FirstOrDefault()?.Nickname ?? "нет"}!" );
                Console.WriteLine( "\nНажмите любую клавишу для возврата в меню..." );
                Console.ResetColor();
                Console.ReadKey();
                MainMenu.ShowMainMenu();
            }
        }

        private static void BattleBetweenPair( IFighter fighter1, IFighter fighter2, int roundNumber )
        {
            Console.WriteLine( $"\n=== Бой между {fighter1.Nickname} и {fighter2.Nickname} ===" );
            Console.WriteLine( "Нажмите любую клавишу для начала боя..." );
            Console.ReadKey();

            while ( fighter1.Health > 0 && fighter2.Health > 0 )
            {
                StartMenu.ShowGameLogo();
                Console.WriteLine( $"                                                      === РАУНД {roundNumber} ===" );
                Console.WriteLine( $"{fighter1.Nickname}: {fighter1.Health} HP" );
                Console.WriteLine( $"{fighter2.Nickname}: {fighter2.Health} HP\n" );

                IFighter attacker, defender;
                if ( Random.Shared.Next( 2 ) == 0 )
                {
                    attacker = fighter1;
                    defender = fighter2;
                }
                else
                {
                    attacker = fighter2;
                    defender = fighter1;
                }

                Console.WriteLine( $"Атакует: {attacker.Nickname}" );
                Console.WriteLine( "Нажмите любую клавишу для атаки..." );
                Console.ReadKey();

                var damage = attacker.CalculateDamage();
                defender.TakeDamage( damage );

                Console.WriteLine( $"\n{attacker.Nickname} наносит {damage} урона" );
                Console.WriteLine( $"{defender.Nickname} теперь имеет {defender.Health} HP" );

                if ( defender.Health <= 0 )
                {
                    Console.WriteLine( $"\n{defender.Nickname} повержен!" );
                }
                Console.WriteLine( "\nНажмите любую клавишу для продолжения..." );
                Console.ReadKey();
            }
        }
    }
}