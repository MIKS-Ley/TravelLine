using Perevodchik.Menu;
using Perevodchik.Services;

class Program
{
    private static void Main( string[] args )
    {
        var dictionaryManager = new DictionaryManager();
        var translatorService = new TranslatorService( dictionaryManager );
        var menuManager = new MenuManager( dictionaryManager, translatorService );

        menuManager.ShowMainMenu();
    }
}