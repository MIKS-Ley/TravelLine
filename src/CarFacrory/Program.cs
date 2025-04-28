using CarFactory.Menu;
using System.Text;

namespace CarFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Car Factory";
            CarMenu.ShowMainMenu();
        }

        public static void ExitGame() => Environment.Exit(0);
    }
}