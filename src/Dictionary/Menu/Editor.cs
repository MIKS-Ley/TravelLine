using Spectre.Console;

namespace Perevodchik
{
    public static class Editor
    {
        public static void SiteName()
        {
            AnsiConsole.Clear();
            var panel = new Panel( TextEmphasis.Custom( Constants.TranslaterText, "white", "green" ) )
                .Border( BoxBorder.None )
                .HeaderAlignment( Justify.Center );

            AnsiConsole.Write( panel );
        }

        public static void DisplayHelpMenu()
        {
            SiteName();

            var helpContent = new Panel( new Markup( Constants.HelpText ) )
                .Border( BoxBorder.Rounded )
                .Header( "[bold green]Справка[/]" )
                .HeaderAlignment( Justify.Center );

            AnsiConsole.Write( helpContent );
            AnsiConsole.MarkupLine( "\n[green]Нажмите любую клавишу для выхода...[/]" );
            AnsiConsole.Console.Input.ReadKey( true );
            AnsiConsole.Clear();
        }

        public static bool AskToAddWord( string word )
        {
            AnsiConsole.MarkupLine( $"\nДобавить слово '[yellow]{word}[/]' в словарь?" );

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices( new[] { "Да", "Нет" } )
                    .HighlightStyle( Style.Parse( "green" ) )
                    .MoreChoicesText( "[grey](Ещё варианты)[/]" )
                    .PageSize( 10 )
            ) == "Да";
        }

        public static void WaitForContinue()
        {
            AnsiConsole.MarkupLine( "\n[green]Нажмите любую клавишу чтобы продолжить...[/]" );
            AnsiConsole.Console.Input.ReadKey( true );
        }
    }
}