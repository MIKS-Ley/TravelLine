using Perevodchik.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perevodchik.Menu
{
    public class MenuManager
    {
        private readonly DictionaryManager _dictionaryManager;
        private readonly TranslatorService _translatorService;

        private class MenuItem
        {
            public string Name { get; }
            public Action Action { get; }

            public MenuItem( string name, Action action )
            {
                Name = name;
                Action = action;
            }

            public override string ToString() => Name;
        }

        public MenuManager( DictionaryManager dictionaryManager, TranslatorService translatorService )
        {
            _dictionaryManager = dictionaryManager;
            _translatorService = translatorService;
            _dictionaryManager.LoadDictionary();
        }

        public void ShowMainMenu()
        {
            var panel = new Panel( "[green]Выполнил: Клыков Михаил.[/]" )
                .Border( BoxBorder.Rounded )
                .Header( "[bold]Translator[/]" )
                .HeaderAlignment( Justify.Center );

            var menuItems = new List<MenuItem>
            {
                new MenuItem("Переводчик", StartTranslator),
                new MenuItem("Редактор словаря", ShowDictionaryEditorMenu),
                new MenuItem("Помощь", Editor.DisplayHelpMenu),
                new MenuItem("Выход", () => Environment.Exit(0))
            };

            while ( true )
            {
                AnsiConsole.Clear();
                AnsiConsole.Write( panel );

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuItem>()
                        .Title( "Выберите действие:" )
                        .PageSize( 10 )
                        .HighlightStyle( Style.Parse( "green" ) )
                        .AddChoices( menuItems )
                        .UseConverter( item => item.Name ) );

                choice.Action?.Invoke();
            }
        }

        private void StartTranslator()
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
                    var translation = _translatorService.Translate( word );
                    var content = new Text( translation ?? $"Слово '{word}' не найдено в словаре.",
                        new Style( translation != null ? Color.Green : Color.Red ) );

                    var panel = new Panel( new Rows( content, new Text( "" ) ) )
                        .Border( BoxBorder.Rounded )
                        .Header( $" Перевод слова {word} " )
                        .HeaderAlignment( Justify.Center );

                    AnsiConsole.Write( panel );

                    if ( translation == null && Editor.AskToAddWord( word ) )
                    {
                        AddNewWordInteraction( word );
                    }
                }

                Editor.WaitForContinue();
            }
        }

        private void ShowDictionaryEditorMenu()
        {
            var editorMenuItems = new List<MenuItem>
            {
                new MenuItem("Удалить словарь", DeleteDictionaryAction),
                new MenuItem("Очистить словарь", ClearDictionaryAction),
                new MenuItem("Добавить слова", () => AddNewWordInteraction()),
                new MenuItem("Просмотр словаря", ViewDictionaryAction),
                new MenuItem("Назад", () => { })
            };

            while ( true )
            {
                AnsiConsole.Clear();
                Editor.SiteName();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuItem>()
                        .Title( "Редактор словаря:" )
                        .PageSize( 10 )
                        .HighlightStyle( Style.Parse( "green" ) )
                        .AddChoices( editorMenuItems )
                        .UseConverter( item => item.Name ) );

                choice.Action?.Invoke();

                if ( choice.Name == "Назад" ) break;
            }
        }

        private void ViewDictionaryAction()
        {
            AnsiConsole.Clear();
            Editor.SiteName();

            try
            {
                var dictionaryData = _dictionaryManager.GetDictionaryContents()
                    .Select( x => (Word: EscapeBrackets( x.Word ), Translation: EscapeBrackets( x.Translation )) )
                    .ToList();

                if ( !dictionaryData.Any() )
                {
                    AnsiConsole.MarkupLine( "[red]Словарь пуст![/]" );
                }
                else
                {
                    var table = new Table()
                        .Border( TableBorder.Rounded )
                        .AddColumn( "[grey]Слово[/]" )
                        .AddColumn( "[grey]Перевод[/]" );

                    foreach ( var item in dictionaryData )
                    {
                        table.AddRow( item.Word, item.Translation );
                    }

                    AnsiConsole.Write( table );
                }
            }
            catch ( Exception ex )
            {
                AnsiConsole.MarkupLine( $"[red]Ошибка: {ex.Message}[/]" );
            }

            AnsiConsole.MarkupLine( "\n[grey]Нажмите любую клавишу для выхода...[/]" );
            AnsiConsole.Console.Input.ReadKey( true );
        }

        private static string EscapeBrackets( string input )
        {
            return input.Replace( "[", "［" ).Replace( "]", "］" );
        }

        private void DeleteDictionaryAction()
        {
            try
            {
                _dictionaryManager.DeleteDictionary();
                _dictionaryManager.ReloadDictionary();
                AnsiConsole.MarkupLine( "[green]Словарь успешно удалён.[/]" );
            }
            catch ( Exception ex )
            {
                AnsiConsole.MarkupLine( $"[red]Ошибка: {ex.Message}[/]" );
            }
            Editor.WaitForContinue();
        }

        private void ClearDictionaryAction()
        {
            try
            {
                _dictionaryManager.ClearDictionary();
                _dictionaryManager.ReloadDictionary();
                AnsiConsole.MarkupLine( "[green]Словарь успешно очищен.[/]" );
            }
            catch ( Exception ex )
            {
                AnsiConsole.MarkupLine( $"[red]Ошибка: {ex.Message}[/]" );
            }
            Editor.WaitForContinue();
        }

        private void AddNewWordInteraction( string word = null )
        {
            AnsiConsole.Clear();
            Editor.SiteName();

            word ??= AnsiConsole.Ask<string>( "Введите новое слово:" ).Trim();
            if ( string.IsNullOrWhiteSpace( word ) )
            {
                AnsiConsole.MarkupLine( "[yellow]Операция отменена: слово не может быть пустым.[/]" );
                Editor.WaitForContinue();
                return;
            }

            if ( _dictionaryManager.RussianToEnglish.ContainsKey( word ) ||
                _dictionaryManager.EnglishToRussian.ContainsKey( word ) )
            {
                AnsiConsole.MarkupLine( $"[yellow]Слово '{word}' уже существует в словаре.[/]" );
                Editor.WaitForContinue();
                return;
            }

            var translation = AnsiConsole.Ask<string>( "Введите перевод:" ).Trim();
            if ( string.IsNullOrWhiteSpace( translation ) )
            {
                AnsiConsole.MarkupLine( "[yellow]Операция отменена: перевод не может быть пустым.[/]" );
                Editor.WaitForContinue();
                return;
            }

            if ( _dictionaryManager.RussianToEnglish.ContainsKey( translation ) ||
                _dictionaryManager.EnglishToRussian.ContainsKey( translation ) )
            {
                AnsiConsole.MarkupLine( $"[yellow]Перевод '{translation}' уже существует в словаре.[/]" );
                Editor.WaitForContinue();
                return;
            }

            if ( _dictionaryManager.AddWord( word, translation ) )
            {
                AnsiConsole.MarkupLine( "[green]Слово успешно добавлено в словарь![/]" );
            }
            else
            {
                AnsiConsole.MarkupLine( "[red]Не удалось добавить слово в словарь.[/]" );
            }

            Editor.WaitForContinue();
        }
    }
}