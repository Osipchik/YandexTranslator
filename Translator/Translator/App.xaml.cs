using System.Collections.Generic;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Translator.Models;
using Translator.ViewModels;
using Translator.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Translator
{
    public partial class App
    { 
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //YandexTranslator.SetAvailableLanguages("ru");

            var asd = new Settings
            {
                Culture = "ru",
                OriginalLang = new KeyValuePair<string, string>("ru", "Русский"),
                TranslateLang = new KeyValuePair<string, string>("en", "Английский")
            };

            var parameters = new NavigationParameters
            {
                {Constants.CultureKey, asd}
            };
            await NavigationService.NavigateAsync("NavigationPage/MainPage", parameters);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectLanguagePage, SelectLanguagePageViewModel>();
        }
    }
}
