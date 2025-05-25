using Spectre.Console;

namespace Perevodchik
{
    public static class Translator
    {
        public static Dictionary<string, string> RussianToEnglish = new Dictionary<string, string>();
        public static Dictionary<string, string> EnglishToRussian = new Dictionary<string, string>();
        public const string FilePath = "dictionary.txt";

        public static void LoadDictionary()
        {
            if ( File.Exists( FilePath ) )
            {
                foreach ( var line in File.ReadAllLines( FilePath ) )
                {
                    var parts = line.Split( ':' );
                    if ( parts.Length == 2 )
                    {
                        var word = parts[ 0 ].Trim();
                        var translation = parts[ 1 ].Trim();
                        RussianToEnglish[ word ] = translation;
                        EnglishToRussian[ translation ] = word;
                    }
                }
            }
            else
            {
                File.WriteAllText( FilePath, $"Файл был создан: {DateTime.Today:dd.MM.yyyy}\n" );
                AnsiConsole.MarkupLine( $"[yellow]Файл '{FilePath}' был создан.[/]" );
            }
        }

        public static void StartTranslator()
        {
            while ( true )
            {
                AnsiConsole.Clear();
                Editor.SiteName();

                var input = AnsiConsole.Prompt(
                    new TextPrompt<string>( "[green]Введите слово для перевода (или '-' для выхода):[/]" )
                        .PromptStyle( "green" ) );

                if ( input == "-" ) break;

                foreach ( var word in input.Split( new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ) )
                {
                    TranslateWord( word );
                }

                Editor.WaitForContinue();
            }
        }

        private static void TranslateWord( string word )
        {
            var content = new Text( "" );

            if ( RussianToEnglish.TryGetValue( word, out var translation ) )
            {
                content = new Text( $"Английский: {translation}", new Style( Color.Green ) );
            }
            else if ( EnglishToRussian.TryGetValue( word, out var reverseTranslation ) )
            {
                content = new Text( $"Русский: {reverseTranslation}", new Style( Color.Green ) );
            }
            else
            {
                content = new Text( $"Слово '{word}' не найдено в словаре.", new Style( Color.Red ) );

                var notFoundPanel = new Panel(
                        new Rows(
                            content,
                            new Text( "" ) )
                    )
                    .Border( BoxBorder.Rounded )
                    .Header( $" Перевод слова {word} " )
                    .HeaderAlignment( Justify.Center );

                AnsiConsole.Write( notFoundPanel );

                if ( Editor.AskToAddWord( word ) )
                {
                    AddNewWord( word );
                }
                return;
            }

            var panel = new Panel(
                    new Rows(
                        content,
                        new Text( "" ) )
                )
                .Border( BoxBorder.Rounded )
                .Header( $" Перевод слова {word} " )
                .HeaderAlignment( Justify.Center );

            AnsiConsole.Write( panel );
        }

        public static bool AddNewWord( string word = null, Func<string, string> getTranslation = null )
        {
            getTranslation ??= prompt => AnsiConsole.Ask<string>( $"[green]{prompt}[/]" );

            word ??= getTranslation( "Введите новое слово:" );
            var translation = getTranslation( "Введите перевод:" );

            if ( !RussianToEnglish.ContainsKey( word ) && !EnglishToRussian.ContainsKey( translation ) )
            {
                RussianToEnglish[ word ] = translation;
                EnglishToRussian[ translation ] = word;
                SaveWordToFile( word, translation );

                AnsiConsole.MarkupLine( $"[green]Слово '{word}' успешно добавлено в словарь![/]" );
                return true;
            }

            AnsiConsole.MarkupLine( $"[red]Слово или перевод уже существуют в словаре.[/]" );
            return false;
        }

        private static void SaveWordToFile( string word, string translation )
        {
            File.AppendAllText( FilePath, $"{word}:{translation}\n" );
        }
    }
}