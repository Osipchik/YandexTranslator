using System.Windows.Input;
using Xamarin.Forms;

namespace Translator.Renderers
{
    public class ClickAbleListView : ListView
    {
        public static readonly BindableProperty ItemClickCommandProperty = BindableProperty.Create(
            nameof(ItemClickCommand),
            typeof(ICommand), 
            typeof(ClickAbleListView));

        public ICommand ItemClickCommand
        {
            get => (ICommand) GetValue(ItemClickCommandProperty);
            set => SetValue(ItemClickCommandProperty, value);
        }

        public ClickAbleListView()
        {
            ItemTapped += ClickAbleListView_ItemTapped;
        }

        private void ClickAbleListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                ItemClickCommand?.Execute(e.Item);
            }
        }
    }
}
