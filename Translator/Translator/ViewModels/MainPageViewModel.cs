using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading.Svg.Forms;
using Prism.Services;
using Translator.Models;
using Xamarin.Forms;
using YandexTranslator;

namespace Translator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _culture;
        private IPageDialogService _pageDialog;

        private KeyValuePair<string, string> _original;
        public KeyValuePair<string,string> Original
        {
            get => _original;
            set => SetProperty(ref _original, value);
        }

        private KeyValuePair<string, string> _translate;
        public KeyValuePair<string, string> Translate
        {
            get => _translate;
            set => SetProperty(ref _translate, value);
        }

        private string _translation;
        public string Translation
        {
            get => _translation;
            set => SetProperty(ref _translation, value);
        }

        private string _textToTranslate;
        public string TextToTranslate
        {
            get => _textToTranslate;
            set => SetProperty(ref _textToTranslate, value);
        }

        private YandexTranslator.YandexTranslator translator;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            :base(navigationService)
        {
            _pageDialog = pageDialog;
            Title = "Yandex Переводчик";
            RotateImageCommand = new DelegateCommand<SvgCachedImage>(RotateImage);
            SetOriginalLanguageCommand = new DelegateCommand(() => ToSelectLanguagePage(Constants.OriginalKey));
            SetTranslateLanguageCommand = new DelegateCommand(() => ToSelectLanguagePage(Constants.TranslateKey));
            TranslateCommand = new DelegateCommand(TranslateText);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Settings settings = parameters.GetValue<Settings>(Constants.CultureKey);
            _culture = settings.Culture;
            Original = settings.OriginalLang;
            Translate = settings.TranslateLang;
            translator = new YandexTranslator.YandexTranslator((string)Application.Current.Resources["ApiKey"], _culture);
        }

        public DelegateCommand<SvgCachedImage> RotateImageCommand { get; }
        public DelegateCommand SetOriginalLanguageCommand { get; }
        public DelegateCommand SetTranslateLanguageCommand { get; }
        public DelegateCommand TranslateCommand { get; }

        private async void RotateImage(SvgCachedImage image)
        {
            await Task.Run(SwapLanguages);
            await image.RotateTo(270).ContinueWith(task => { image.Rotation = 90; });
        }

        private void SwapLanguages()
        {
            var keyValue = _original;
            Original = Translate;
            Translate = keyValue;
        }

        private async void ToSelectLanguagePage(string key)
        {
            var parameters = new NavigationParameters{{LanguageKey, key}, {LanguagesKey, translator.Languages}};
            await NavigationService.NavigateAsync(SelectLanguagePageKey, parameters);
        }

        private async void TranslateText()
        {
            if (!string.IsNullOrEmpty(TextToTranslate)){
                var translation = await translator.TranslateTextAsync(TextToTranslate, Original.Key, Translate.Key);
                switch (translation.Code)
                {
                    case (int)YandexCode.Ok:
                        Translation = string.Join(" ", translation.Text);
                        break;
                    case (int)YandexCode.WrongKey:
                        await _pageDialog.DisplayAlertAsync("warning", "wrong API key", "close");
                        break;
                    case (int)YandexCode.ApiBlocked:
                        await _pageDialog.DisplayAlertAsync("warning", "API is blocked", "close");
                        break;
                    case (int)YandexCode.IsNotSupported:
                        await _pageDialog.DisplayAlertAsync("warning", "We cannot translate into this language", "close");
                        break;
                    case (int)YandexCode.Limit:
                        await _pageDialog.DisplayAlertAsync("warning", "Limit of translation", "close");
                        break;
                    case (int)YandexCode.Size:
                        await _pageDialog.DisplayAlertAsync("warning", "The text is too long (> 10000)", "close");
                        break;
                    default:
                        await _pageDialog.DisplayAlertAsync("warning", "Something went wrong", "close");
                        break;
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Original.Value != null){
                SetOriginalLanguage(parameters);
                SetTranslateLanguage(parameters);
            }
        }

        private void SetOriginalLanguage(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.OriginalKey)){
                Original = parameters.GetValue<KeyValuePair<string, string>>(Constants.OriginalKey);
            }
        }

        private void SetTranslateLanguage(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.TranslateKey)){
                Translate = (KeyValuePair<string, string>)parameters[Constants.TranslateKey];
            }
        }
    }
}