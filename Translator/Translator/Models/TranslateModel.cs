using System.Collections.Generic;

namespace Translator.Models
{
    public class TranslateModel
    {
        public int Code { get; set; }
        public IDictionary<string, string> Detected { get; set; }
        public string Lang { get; set; }
        public IList<string> Text { get; set; }
    }
}
