using System.Windows.Input;
using Xamarin.Forms;

namespace Translator.Renderers
{
    public class CleanEditor : Editor
    {
        public static readonly BindableProperty OnFocusedCommandProperty = BindableProperty.Create(
            nameof(OnFocusedCommand),
            typeof(ICommand),
            typeof(CleanEditor));

        public static readonly BindableProperty OnUnFocusedCommandProperty = BindableProperty.Create(
            nameof(OnUnFocusedCommand),
            typeof(ICommand),
            typeof(CleanEditor));

        public ICommand OnFocusedCommand
        {
            get => (ICommand) GetValue(OnFocusedCommandProperty);
            set => SetValue(OnFocusedCommandProperty, value);
        }

        public ICommand OnUnFocusedCommand
        {
            get => (ICommand)GetValue(OnUnFocusedCommandProperty);
            set => SetValue(OnUnFocusedCommandProperty, value);
        }

        public CleanEditor()
        {
            Focused += OnFocused;
            Unfocused += OnUnfocused;
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            if (!e.IsFocused)
            {
                OnUnFocusedCommand?.Execute(e.IsFocused);
            }
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                OnFocusedCommand?.Execute(e.IsFocused);
            }
        }
    }
}
