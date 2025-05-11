using System;
using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public class PlayerConfig : IFighter
    {
        public int Id { get; set; }
        public string Race { get; set; }
        public string Nickname { get; set; }
        public string Armor { get; set; }
        public string Weapon { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int ArmorDurability { get; set; }
        public int WeaponDamage { get; set; }
        public int Initiative { get; set; }

        public string Name => Nickname;

        public int GetCurrentHealth() => Health;
        public bool IsAlive() => Health > 0;

        public int CalculateDamage()
        {
            int baseDamage = WeaponDamage + Strength / 2;
            double variation = 1.0 + ( Random.Shared.NextDouble() * 0.3 - 0.2 );
            int damage = ( int )( baseDamage * variation );

            if ( Random.Shared.Next( 100 ) < 10 )
            {
                damage *= 2;
                Console.Write( "Критический удар! " );
            }

            return damage;
        }

        public void TakeDamage( int damage )
        {
            int armorReduction = ArmorDurability / 2;
            int actualDamage = Math.Max( 1, damage - armorReduction );
            Health = Math.Max( 0, Health - actualDamage );
        }
    }
}