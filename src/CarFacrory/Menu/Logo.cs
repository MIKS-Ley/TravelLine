using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void MainLogo()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(MainLogoText);
            Console.ResetColor();
        }
    }
}