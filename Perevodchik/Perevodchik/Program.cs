using System.Text;
class Program
{
    private static Dictionary<string, string> russianToEnglish = new Dictionary<string, string>();
    private static Dictionary<string, string> englishToRussian = new Dictionary<string, string>();
    private const string filePath = "dictionary.txt";

    static void Main( string[] args )
    {
        LoadDictionary();
        string[] menu = { "Переводчик", "Редактор словаря", "Помощь", "Выход" };
        int select = 0;

        while ( true )
        {
            Console.Clear();
            Console.Title = "Translator";
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine( "Выполнил: Клыков Михаил." );
            Console.WriteLine( "Выберите действие:" );
            Console.ResetColor();

            for ( int i = 0; i < menu.Length; i++ )
            {
                if ( i == select )
                {
                    Console.Write( "> " );
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.Write( "  " );
                }

                Console.WriteLine( menu[ i ] );
                Console.ResetColor();
            }

            var key = Console.ReadKey( true ).Key;

            if ( key == ConsoleKey.UpArrow && select > 0 ) select--;
            if ( key == ConsoleKey.DownArrow && select < menu.Length - 1 ) select++;

            if ( key == ConsoleKey.Enter )
            {
                if ( select == 0 ) StartTranslator(); // Запустить переводчик
                if ( select == 1 ) DictionaryEditor(); // Редактор словаря
                if ( select == 2 ) DisplayHelpMenu(); // Помощь
                if ( select == 3 ) return; // Выход
            }
        }
    }

    private static void LoadDictionary() // Загрузка словаря
    {

        if ( File.Exists( filePath ) )
        {
            var lines = File.ReadAllLines( filePath );
            foreach ( var line in lines )
            {
                var parts = line.Split( ':' );
                if ( parts.Length == 2 )
                {
                    var word = parts[ 0 ].Trim();
                    var translation = parts[ 1 ].Trim();
                    russianToEnglish[ word ] = translation;
                    englishToRussian[ translation ] = word;
                }
            }
        }
        else // Если не нашел файл создаем его
        {
            {
                using ( FileStream fs = File.Create( filePath ) )
                {
                    DateTime todayDate = DateTime.Today;
                    byte[] info = new UTF8Encoding( true ).GetBytes( $"Файл был создан: {todayDate:dd.MM.yyyy}\n" );
                    fs.Write( info, 0, info.Length );
                }

                Console.WriteLine( $"Файл '{filePath}' был создан." );
            }
        }

    }

    private static void StartTranslator() // Алгоритм работы переводчика
    {
        while ( true )
        {
            SiteName();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( "Выход клавиша -> '-'" );
            Console.Write( "Введите слово для перевода: " );
            var input = Console.ReadLine();

            if ( input == "-" ) // Выход
            {
                break;
            }
            var words = input.Split( new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
            foreach ( var word in words )
            {
                if ( russianToEnglish.TryGetValue( word, out var translation ) )
                {
                    Console.WriteLine( $"Перевод '{word}': {translation}" );
                }
                else if ( englishToRussian.TryGetValue( word, out var reverseTranslation ) )
                {
                    Console.WriteLine( $"Перевод '{word}': {reverseTranslation}" );
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine( $"Слово '{word}' не найдено в словаре." );
                    Console.ResetColor();
                    string[] menu = { "Да", "Нет" };
                    int select = 0;
                    int menuTopPosition = Console.CursorTop;

                    while ( true )
                    {
                        int currentLeft = Console.CursorLeft;
                        int currentTop = Console.CursorTop;

                        Console.SetCursorPosition( 0, menuTopPosition );

                        for ( int i = 0; i < menu.Length; i++ )
                        {
                            Console.Write( new string( ' ', Console.WindowWidth ) );
                            Console.SetCursorPosition( 0, menuTopPosition + i );

                            if ( i == select )
                            {
                                Console.Write( "> " );
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.Write( "  " );
                            }

                            Console.Write( menu[ i ] );
                            Console.ResetColor();
                        }

                        Console.SetCursorPosition( currentLeft, currentTop );

                        var key = Console.ReadKey( true ).Key;

                        if ( key == ConsoleKey.UpArrow && select > 0 ) select--;
                        if ( key == ConsoleKey.DownArrow && select < menu.Length - 1 ) select++;
                        if ( key == ConsoleKey.LeftArrow ) select = 0;
                        if ( key == ConsoleKey.RightArrow ) select = 1;

                        if ( key == ConsoleKey.Enter )
                        {
                            Console.SetCursorPosition( 0, menuTopPosition );
                            for ( int i = 0; i < menu.Length; i++ )
                            {
                                Console.Write( new string( ' ', Console.WindowWidth ) );
                                if ( i < menu.Length - 1 )
                                    Console.SetCursorPosition( 0, menuTopPosition + i + 1 );
                            }

                            Console.SetCursorPosition( 0, menuTopPosition );

                            if ( select == 0 ) // если да
                            {
                                AddNewWord( word );
                            }
                            else // // Возврат к переводу следующих слов
                            {

                            }
                            break;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( "Нажмите Enter чтобы продолжить" );
            Console.ReadLine();
            Console.ResetColor();
        }
    }

    private static void AddNewWord( string? word = null ) // Добавление новых слов 
    {
        Console.ForegroundColor = ConsoleColor.Green;
        if ( word == null )
        {
            Console.Write( "Введите новое слово: " );
            word = Console.ReadLine();
        }
        Console.Write( "Введите перевод: " );
        var translation = Console.ReadLine();

        if ( !russianToEnglish.ContainsKey( word ) && !englishToRussian.ContainsKey( translation ) )
        {
            russianToEnglish[ word ] = translation;
            englishToRussian[ translation ] = word;
            SaveWordToFile( word, translation );
            Console.WriteLine( "Слово добавлено в словарь." );
        }
        else
        {
            Console.WriteLine( "Это слово уже существовало в словаре." );
            Console.Write( "Нажмите любую клавишу для выхода..." );
            Console.ReadKey();
        }
        Console.ResetColor();
    }
    static void SaveWordToFile( string word, string translation ) //Сохранение новых слов в словарь
    {
        using ( StreamWriter writer = new StreamWriter( "dictionary.txt", true ) )
        {
            writer.WriteLine( $"{word}:{translation}" );
        }
    }
    static void SiteName() //Заголовок программы
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine( $@"
$$$$$$$$\                                         $$\             $$\                         
\__$$  __|                                        $$ |            $$ |                        
   $$ |    $$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$$\ $$ | $$$$$$\  $$$$$$\    $$$$$$\   $$$$$$\  
   $$ |   $$  __$$\  \____$$\ $$  __$$\ $$  _____|$$ | \____$$\ \_$$  _|  $$  __$$\ $$  __$$\ 
   $$ |   $$ |  \__| $$$$$$$ |$$ |  $$ |\$$$$$$\  $$ | $$$$$$$ |  $$ |    $$ /  $$ |$$ |  \__|
   $$ |   $$ |      $$  __$$ |$$ |  $$ | \____$$\ $$ |$$  __$$ |  $$ |$$\ $$ |  $$ |$$ |      
   $$ |   $$ |      \$$$$$$$ |$$ |  $$ |$$$$$$$  |$$ |\$$$$$$$ |  \$$$$  |\$$$$$$  |$$ |      
   \__|   \__|       \_______|\__|  \__|\_______/ \__| \_______|   \____/  \______/ \__|      
                                                                                              " );
        Console.WriteLine();
        Console.ResetColor();
    }

    static void DeleteDictionary() // Удаление словаря
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        if ( File.Exists( filePath ) )
        {
            try
            {
                File.Delete( filePath );
                Console.WriteLine( $"Файл {filePath} был успешно удален." );

            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Ошибка при удалении файла: {ex.Message}" );
            }
            Console.Write( "Нажмите любую клавишу для выхода..." );
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine( $"Файл {filePath} не найден." );
        }
        Console.ResetColor();
    }
    static void ClearDictionary() // Очистка словаря
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        try
        {
            using ( StreamWriter writer = new StreamWriter( filePath, false ) )
            {
                Console.WriteLine( $"Файл {filePath} был успешно очищен." );
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при очистке файла: {ex.Message}" );
        }
        Console.Write( "Нажмите любую клавишу для выхода..." );
        Console.ReadKey();
    }
    static void WordsNow()
    {
        Console.Clear();
        AddNewWord();
    }
    static void DisplayDictionary()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        string filePath = "dictionary.txt";
        try
        {
            if ( File.Exists( filePath ) )
            {
                using ( StreamReader reader = new StreamReader( filePath ) )
                {
                    string? line;
                    while ( ( line = reader.ReadLine() ) != null )
                    {
                        Console.WriteLine( line );
                    }
                }
            }
            else
            {
                Console.WriteLine( $"Файл {filePath} не найден." );
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при чтении файла: {ex.Message}" );
        }
        Console.Write( "Нажмите любую клавишу для выхода..." );
        Console.ReadKey();
        Console.ResetColor();
    }

    static void DictionaryEditor()
    {
        Console.Clear();
        string[] editor = { "- Удалить словарь", "- Очистить словарь", "- Добавить слова", "- Просмотр словаря", "- Выход" }; // Определяем пункты меню
        int select = 0;

        while ( true )
        {
            SiteName();

            for ( int i = 0; i < editor.Length; i++ )
            {
                if ( i == select )
                {
                    Console.Write( "> " );
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.Write( "  " );
                }

                Console.WriteLine( editor[ i ] );
                Console.ResetColor();
            }

            var key = Console.ReadKey( true ).Key;

            if ( key == ConsoleKey.UpArrow && select > 0 ) select--;
            if ( key == ConsoleKey.DownArrow && select < editor.Length - 1 ) select++;
            if ( key == ConsoleKey.Enter )
            {
                if ( select == 0 ) DeleteDictionary(); // Удалить словарь
                if ( select == 1 ) ClearDictionary(); // Очистить словарь
                if ( select == 2 ) WordsNow(); // Добавить слова
                if ( select == 3 ) DisplayDictionary(); // Просмотр словаря
                if ( select == 4 ) return; // Выход
            }
        }
    }

    static void DisplayHelpMenu() // Решил добавить информацию для корректного использование программы 
    {
        SiteName();
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine( $@"
                            Программа переводчик.                               
 Здравствуйте! Функционал данной программы заключается в переводе слов          
 с русского языка на английский и наоборот.                                     
                                                                                
 Перевод слов берётся из вашего словаря, который вы можете редактировать.       
 Но добавление слов без перевода или некорректное его написание она не разрешит.
 Также вы можете:                                                               
                                                                                
 - Очищать словарь                                                              
 - Добавлять слова                                                              
 - Удалять словарь                                                              
 - Вывод всего словаря на экран.                                                
 - Перевод нескольних слов однавременно                                         " );
        Console.ResetColor();
        Console.WriteLine( "\nНажмите любую клавишу для выхода..." );
        Console.ReadKey();
        Console.Clear();
    }
}

