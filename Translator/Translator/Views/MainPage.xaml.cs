using System;
using Xamarin.Forms;

namespace Translator.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Editor_OnFocused(object sender, FocusEventArgs e)
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void Editor_OnUnfocused(object sender, FocusEventArgs e)
        {
            NavigationPage.SetHasNavigationBar(this, true);
        }
    }
}