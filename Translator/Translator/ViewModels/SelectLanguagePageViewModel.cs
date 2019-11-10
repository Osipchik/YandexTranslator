using Prism.Commands;
using Prism.Navigation;
using Translator.Models;
using Translator.Translator;

namespace Translator.ViewModels
{
    public class SelectLanguagePageViewModel : ViewModelBase
    {
        private string _selectLanguageKey;

        private static AvailableLanguages _languages;
        public AvailableLanguages Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        public SelectLanguagePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Languages = YandexTranslator.Languages;
            ItemClickCommand = new DelegateCommand<object>((item) => Command(item));
        }

        public DelegateCommand<object> ItemClickCommand { get; set; }

        private async void Command(object item)
        {
            var navigationParameters = new NavigationParameters {{ _selectLanguageKey, item } };
            await NavigationService.GoBackAsync(navigationParameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _selectLanguageKey = parameters.GetValue<string>(LanguageKey);
        }
    }
}
