using Spectre.Console;

namespace Perevodchik
{
    public static class MenuManager
    {
        public static void ShowMainMenu()
        {
            var options = new List<string> { "Переводчик", "Редактор словаря", "Помощь", "Выход" };
            var actions = new List<Action>
            {
                Translator.StartTranslator,
                ShowDictionaryEditorMenu,
                Editor.DisplayHelpMenu,
                () => Environment.Exit(0)
            };

            var panel = new Panel( "[green]Выполнил: Клыков Михаил.[/]" )
                .Border( BoxBorder.Rounded )
                .Header( "[bold]Translator[/]" )
                .HeaderAlignment( Justify.Center );

            while ( true )
            {
                AnsiConsole.Clear();
                AnsiConsole.Write( panel );

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title( "Выберите действие:" )
                        .PageSize( 10 )
                        .HighlightStyle( Style.Parse( "green" ) )
                        .AddChoices( options ) );

                int selectedIndex = options.IndexOf( choice );
                actions[ selectedIndex ]?.Invoke();
            }
        }

        private static void ShowDictionaryEditorMenu()
        {
            var options = new List<string>
            {
                "Удалить словарь",
                "Очистить словарь",
                "Добавить слова",
                "Просмотр словаря",
                "Назад"
            };

            var actions = new List<Action>
            {
                () => DictionaryEditor.DeleteDictionary(Translator.RussianToEnglish, Translator.EnglishToRussian),
                () => DictionaryEditor.ClearDictionary(Translator.RussianToEnglish, Translator.EnglishToRussian),
                AddNewWordInteraction,
                DictionaryEditor.DisplayDictionary,
                ShowMainMenu
            };

            while ( true )
            {
                AnsiConsole.Clear();
                Editor.SiteName();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title( "Редактор словаря:" )
                        .PageSize( 10 )
                        .HighlightStyle( Style.Parse( "green" ) )
                        .AddChoices( options ) );

                int selectedIndex = options.IndexOf( choice );
                actions[ selectedIndex ]?.Invoke();
            }
        }

        private static void AddNewWordInteraction()
        {
            AnsiConsole.Clear();
            Editor.SiteName();

            var word = AnsiConsole.Ask<string>( "Введите новое слово:" ).Trim();
            if ( string.IsNullOrWhiteSpace( word ) ) return;

            if ( Translator.RussianToEnglish.ContainsKey( word ) ||
                Translator.EnglishToRussian.ContainsKey( word ) )
            {
                AnsiConsole.MarkupLine( "[yellow]Это слово уже существует в словаре.[/]" );
                AnsiConsole.Console.Input.ReadKey( true );
                return;
            }

            var translation = AnsiConsole.Ask<string>( "Введите перевод:" ).Trim();
            if ( string.IsNullOrWhiteSpace( translation ) ) return;

            if ( Translator.AddNewWord( word, prompt =>
            {
                return AnsiConsole.Ask<string>( prompt );
            } ) )
            {
                AnsiConsole.MarkupLine( "[green]Слово добавлено в словарь.[/]" );
            }
            else
            {
                AnsiConsole.MarkupLine( "[red]Не удалось добавить слово.[/]" );
            }

            AnsiConsole.Console.Input.ReadKey( true );
        }
    }
}