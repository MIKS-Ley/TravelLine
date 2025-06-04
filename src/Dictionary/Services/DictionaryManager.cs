using System.Text;

namespace Perevodchik.Services
{
    public class DictionaryManager
    {
        public string FilePath { get; } = "dictionary.txt";
        private readonly Dictionary<string, string> _russianToEnglish = new();
        private readonly Dictionary<string, string> _englishToRussian = new();

        public DictionaryManager()
        {
            LoadDictionary();
        }

        public IReadOnlyDictionary<string, string> RussianToEnglish => _russianToEnglish;
        public IReadOnlyDictionary<string, string> EnglishToRussian => _englishToRussian;

        public IEnumerable<(string Word, string Translation)> GetDictionaryContents()
        {
            if ( !File.Exists( FilePath ) )
            {
                yield return ("Словарь не найден", "");
                yield break;
            }

            foreach ( var line in File.ReadLines( FilePath ) )
            {
                if ( string.IsNullOrWhiteSpace( line ) ) continue;

                var parts = line.Split( ':' );
                if ( parts.Length == 2 )
                {
                    yield return (parts[ 0 ].Trim(), parts[ 1 ].Trim());
                }
                else if ( !line.StartsWith( "Файл был создан" ) )
                {
                    yield return (line, "-");
                }
            }
        }

        public void LoadDictionary()
        {
            if ( !File.Exists( FilePath ) )
            {
                File.WriteAllText( FilePath, $"Файл был создан: {DateTime.Today:dd.MM.yyyy}\n" );
                return;
            }

            foreach ( var line in File.ReadAllLines( FilePath ) )
            {
                var parts = line.Split( ':' );
                if ( parts.Length == 2 )
                {
                    var word = parts[ 0 ].Trim();
                    var translation = parts[ 1 ].Trim();

                    if ( IsRussian( word ) )
                    {
                        _russianToEnglish[ word ] = translation;
                        _englishToRussian[ translation ] = word;
                    }
                    else
                    {
                        _englishToRussian[ word ] = translation;
                        _russianToEnglish[ translation ] = word;
                    }
                }
            }
        }

        public bool AddWord( string word, string translation )
        {
            if ( string.IsNullOrWhiteSpace( word ) || string.IsNullOrWhiteSpace( translation ) )
                return false;

            if ( _russianToEnglish.ContainsKey( word ) || _englishToRussian.ContainsKey( word ) ||
                _russianToEnglish.ContainsKey( translation ) || _englishToRussian.ContainsKey( translation ) )
                return false;

            if ( IsRussian( word ) )
            {
                _russianToEnglish[ word ] = translation;
                _englishToRussian[ translation ] = word;
            }
            else
            {
                _englishToRussian[ word ] = translation;
                _russianToEnglish[ translation ] = word;
            }

            File.AppendAllText( FilePath, $"{word}:{translation}\n" );
            return true;
        }

        public void ClearDictionary()
        {
            _russianToEnglish.Clear();
            _englishToRussian.Clear();
            File.WriteAllText( FilePath, string.Empty );
        }

        public void DeleteDictionary()
        {
            _russianToEnglish.Clear();
            _englishToRussian.Clear();
            if ( File.Exists( FilePath ) )
                File.Delete( FilePath );
        }

        public void ReloadDictionary()
        {
            _russianToEnglish.Clear();
            _englishToRussian.Clear();
            LoadDictionary();
        }

        private static bool IsRussian( string text )
        {
            return text.Any( c => c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || c == 'ё' || c == 'Ё' );
        }
    }
}