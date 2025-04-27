namespace Fighters.Models.Armors
{
    public class LeatherArmor : IArmor
    {
        public int Armor => 15;
        public ConsoleColor? Color => ConsoleColor.DarkYellow;
    }
}