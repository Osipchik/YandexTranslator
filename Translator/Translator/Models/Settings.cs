using System.Collections.Generic;

namespace Translator.Models
{
    public class Settings
    {
        public string Culture { get; set; }
        public KeyValuePair<string,string> OriginalLang { get; set; }
        public KeyValuePair<string, string> TranslateLang { get; set; }
    }
}
