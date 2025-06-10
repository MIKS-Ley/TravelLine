namespace CarFactory.Menu
{
    public class Logo
    {
        public const string MainLogoText = $@"
 ######     ###    ########        ########    ###     ######  ########  #######  ########  ##    ## 
##    ##   ## ##   ##     ##       ##         ## ##   ##    ##    ##    ##     ## ##     ##  ##  ##  
##        ##   ##  ##     ##       ##        ##   ##  ##          ##    ##     ## ##     ##   ####   
##       ##     ## ########        ######   ##     ## ##          ##    ##     ## ########     ##    
##       ######### ##   ##         ##       ######### ##          ##    ##     ## ##   ##      ##    
##    ## ##     ## ##    ##        ##       ##     ## ##    ##    ##    ##     ## ##    ##     ##    
 ######  ##     ## ##     ##       ##       ##     ##  ######     ##     #######  ##     ##    ##    ";

        public static void ShowMainLogo()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine( MainLogoText );
            Console.ResetColor();
        }
    }
}