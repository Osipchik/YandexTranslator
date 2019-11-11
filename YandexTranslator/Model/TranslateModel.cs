using System.Collections.Generic;

namespace YandexTranslator.Model
{
    public class TranslateModel
    {
        public int Code { get; set; }
        public IDictionary<string, string> Detected { get; set; }
        public string Language { get; set; }
        public IList<string> Text { get; set; }
    }
}
