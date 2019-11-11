using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YandexTranslator.Model;

namespace YandexTranslator
{
    public enum YandexCode
    {
        Ok = 200,
        WrongKey = 401,
        ApiBlocked = 402,
        Limit = 404,
        Size = 413,
        CantTranslate = 422,
        IsNotSupported = 501
    }

    public class YandexTranslator
    {
        private readonly string _apiKey;
        public string Culture;

        public YandexTranslator(string apiKey, string culture)
        {
            _apiKey = "?key=" + apiKey;
            Culture = culture;
            SetAvailableLanguagesAsync();
        }


        public IDictionary<string, string> Languages { get; private set; }

        public async void SetAvailableLanguagesAsync() => await Task.Run(() => { Languages = GetAvailableLanguages(Culture); });

        private IDictionary<string, string> GetAvailableLanguages(string uiCulture)
        {
            var request = CreateRequest(YandexConstants.OptionGetLangs, null, null, uiCulture);
            return JsonConvert.DeserializeObject<AvailableLanguages>(GetJson(request)).Langs;
        }



        public void GetDetectedLanguage(string text)
        {
            var request = CreateRequest(YandexConstants.OptionDetect, text);

            var json = GetJson(request);

        }



        public async Task<TranslateModel> TranslateTextAsync(string text, string originalLanguage, string targetLanguage)
        {
            var request = CreateRequest(YandexConstants.OptionTranslate, text, originalLanguage + "-" + targetLanguage);
            return await Task.Run(() => JsonConvert.DeserializeObject<TranslateModel>(GetJson(request)));
        }

        private WebRequest CreateRequest(string option, string text = null, string translateLangs = null, string uiCulture = null)
        {
            var asd = new StringBuilder(YandexConstants.TranslatorUrl + option + _apiKey);
            if (text == null){
                asd.AppendLine(YandexConstants.Ui + uiCulture + YandexConstants.Lang + "langs");
            }
            else if (translateLangs != null){
                asd.AppendLine(YandexConstants.Text + text + YandexConstants.Lang + translateLangs + YandexConstants.Format + YandexConstants.Options);
            }
            else{
                asd.AppendLine(YandexConstants.Text + text);
            }

            return WebRequest.Create(asd.ToString());
        }

        private string GetJson(WebRequest request)
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
