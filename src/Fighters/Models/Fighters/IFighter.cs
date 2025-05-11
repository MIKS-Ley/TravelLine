using System;

namespace Fighters.Models.Fighters
{
    public interface IFighter
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

        public int CalculateDamage();
        public void TakeDamage( int damage );
    }
}