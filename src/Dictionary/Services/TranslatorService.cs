using Spectre.Console;

namespace Perevodchik.Services
{
    public class TranslatorService
    {
        private readonly DictionaryManager _dictionaryManager;

        public TranslatorService( DictionaryManager dictionaryManager )
        {
            _dictionaryManager = dictionaryManager;
        }

        public string Translate( string word )
        {
            if ( _dictionaryManager.RussianToEnglish.TryGetValue( word, out var englishTranslation ) )
            {
                return $"Английский: {englishTranslation}";
            }

            if ( _dictionaryManager.EnglishToRussian.TryGetValue( word, out var russianTranslation ) )
            {
                return $"Русский: {russianTranslation}";
            }

            return null;
        }

        public bool TryAddWord( string word, string translation )
        {
            return _dictionaryManager.AddWord( word, translation );
        }
    }
}