using Perevodchik;

class Program
{
    private static void Main( string[] args )
    {
        Translator.LoadDictionary();
        MenuManager.ShowMainMenu();
    }
}