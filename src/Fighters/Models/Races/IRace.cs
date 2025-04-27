namespace Fighters.Models.Races
{
    public interface IRace
    {
        public int Damage { get; }
        public int Health { get; }
        public int Armor { get; }
        public string Face { get; }
        public string Body { get; }
    }
}