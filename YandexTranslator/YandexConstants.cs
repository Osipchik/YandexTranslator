namespace YandexTranslator
{
    public static class YandexConstants
    {
        public const string TranslatorUrl = "https://translate.yandex.net/api/v1.5/tr.json/";
        public const string Format = "&format=plain";
        public const string Options = "&options=1";
        public const string Lang = "&lang=";
        public const string Text = "&text=";
        public const string Ui = "&ui=";

        public const string OptionTranslate = "translate";
        public const string OptionGetLangs = "getLangs";
        public const string OptionDetect = "detect";
    }
}
