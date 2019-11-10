using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Translator.Models;

namespace Translator.Translator
{
    public class YandexTranslator
    {
        private const string TranslatorUrl = "https://translate.yandex.net/api/v1.5/tr.json/";
        private const string Key = "?key=trnsl.1.1.20191108T075346Z.68c40f40f480229b.09c899e7170e4f8716de5ef26d42a7eaa4068f38";
        private const string Format = "&format=plain";
        private const string Options = "&options=1";
        private const string Lang = "&lang=";
        private const string Text = "&text=";
        private const string Ui = "&ui=";

        private const string OptionTranslate = "translate";
        private const string OptionGetLangs = "getLangs";
        private const string OptionDetect = "detect";

        public static AvailableLanguages Languages { get; private set; }

        public static async void SetAvailableLanguages(string culture) =>
            await Task.Run(() => { Languages = GetAvailableLanguages(culture); });

        private static AvailableLanguages GetAvailableLanguages(string uiCulture)
        {
            var request = CreateRequest(OptionGetLangs, null, null, uiCulture);
            return JsonConvert.DeserializeObject<AvailableLanguages>(GetJson(request));
        }


        public void GetDetectedLanguage(string text)
        {
            var request = CreateRequest(OptionDetect, text);

            var json = GetJson(request);

        }

        private static TranslateModel TranslateText(string text, string lang)
        {
            var request = CreateRequest(OptionTranslate, text, lang);
            return JsonConvert.DeserializeObject<TranslateModel>(GetJson(request));
        }

        public static async Task<TranslateModel> TranslateTextAsync(string text, string lang) =>
            await Task.Run(() => TranslateText(text, lang));

        private static WebRequest CreateRequest(string option, string text = null, string translateLangs = null, string uiCulture = null)
        {
            var asd = new StringBuilder(TranslatorUrl + option + Key);
            if (text == null){
                asd.AppendLine(Ui + uiCulture + Lang + "langs");
            }
            else if (translateLangs != null){
                asd.AppendLine(Text + text + Lang + translateLangs + Format + Options);
            }
            else{
                asd.AppendLine(Text + text);
            }

            return WebRequest.Create(asd.ToString());
        }

        private static string GetJson(WebRequest request)
        {
            string json;
            using (var response = request.GetResponse()){
                using (var stream = new StreamReader(response.GetResponseStream())){
                    json = stream.ReadToEndAsync().Result;
                }
            }

            return json;
        }
    }
}
