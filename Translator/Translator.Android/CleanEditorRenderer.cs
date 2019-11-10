using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Translator.Droid;
using Translator.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CleanEditor), typeof(CleanEditorRenderer))]
namespace Translator.Droid
{
    public class CleanEditorRenderer : EditorRenderer
    {
        public CleanEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackground(gd);
                Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}