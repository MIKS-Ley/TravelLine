using Spectre.Console;

namespace Perevodchik
{
    public static class Editor
    {
        private const string TranslaterText = @"
$$$$$$$$\                                         $$\             $$\                         
\__$$  __|                                        $$ |            $$ |                        
   $$ |    $$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$$\ $$ | $$$$$$\  $$$$$$\    $$$$$$\   $$$$$$\  
   $$ |   $$  __$$\  \____$$\ $$  __$$\ $$  _____|$$ | \____$$\ \_$$  _|  $$  __$$\ $$  __$$\ 
   $$ |   $$ |  \__| $$$$$$$ |$$ |  $$ |\$$$$$$\  $$ | $$$$$$$ |  $$ |    $$ /  $$ |$$ |  \__|
   $$ |   $$ |      $$  __$$ |$$ |  $$ | \____$$\ $$ |$$  __$$ |  $$ |$$\ $$ |  $$ |$$ |      
   $$ |   $$ |      \$$$$$$$ |$$ |  $$ |$$$$$$$  |$$ |\$$$$$$$ |  \$$$$  |\$$$$$$  |$$ |      
   \__|   \__|       \_______|\__|  \__|\_______/ \__| \_______|   \____/  \______/ \__|      
                                                                                              ";

        public static void SiteName()
        {
            AnsiConsole.Clear();
            var panel = new Panel( TextEmphasis.Custom( TranslaterText, "white", "green" ) )
                .Border( BoxBorder.None )
                .HeaderAlignment( Justify.Center );

            AnsiConsole.Write( panel );
        }

        public static void DisplayHelpMenu()
        {
            SiteName();

            var helpContent = new Panel( new Markup( @"
[bold]Программа переводчик[/]
Здравствуйте! Функционал данной программы заключается в переводе слов          
с русского языка на английский и наоборот.                                     
                                                                                
Перевод слов берётся из вашего словаря, который вы можете редактировать.       
Но добавление слов без перевода или некорректное его написание она не разрешит.
                                                                                
[bold underline]Основные возможности:[/]
- Очищать словарь                                                              
- Добавлять слова                                                              
- Удалять словарь                                                              
- Вывод всего словаря на экран                                                
- Перевод нескольких слов одновременно" ) )
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

        public static class TextEmphasis
        {
            public static string Custom( string text, string foreground, string background )
            {
                return $"[{foreground} on {background}]{text}[/]";
            }
        }
    }
}