using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;

namespace Translator.ViewModels
{
    public class SelectLanguagePageViewModel : ViewModelBase
    {
        private string _selectLanguageKey;

        private static IDictionary<string, string> _languages;
        public IDictionary<string, string> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        public SelectLanguagePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ItemClickCommand = new DelegateCommand<object>((item) => Command(item));
        }

        public DelegateCommand<object> ItemClickCommand { get; set; }

        private async void Command(object item)
        {
            var navigationParameters = new NavigationParameters {{ _selectLanguageKey, item } };
            await NavigationService.GoBackAsync(navigationParameters);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Task.Run(() =>
            {
                _selectLanguageKey = parameters.GetValue<string>(LanguageKey);
                Languages = parameters.GetValue<IDictionary<string, string>>(LanguagesKey);
            });
        }
    }
}
