using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Views.InputMethods;
using MobileApp.Controls;
using MobileApp.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedCornerEntry), typeof(RoundedEntryRenderer))]

namespace MobileApp.Droid.CustomRenderer
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gradient = new GradientDrawable();
                var roundedEntry = Element as RoundedCornerEntry;
                var padding = (int)Utils.ConvertDpToPixel(Context, 10);

                if (roundedEntry != null)
                {
                    gradient.SetStroke(roundedEntry.BorderThickness, roundedEntry.BorderColor.ToAndroid());
                    gradient.SetCornerRadius(Utils.ConvertDpToPixel(Context, roundedEntry.BorderRadius));
                    gradient.SetColor(roundedEntry.EntryBackgroundColor.ToAndroid());
                    SetReturnType(roundedEntry);

                    // Editor Action is called when the return button is pressed
                    Control.EditorAction += (sender, args) =>
                    {
                        if (roundedEntry.ReturnType != ReturnType.Next)
                            roundedEntry.Unfocus();

                        // Call all the methods attached to base_entry event handler Completed
                        roundedEntry.InvokeCompleted();
                    };
                }

                if (!roundedEntry.DisplaySuggestions) Control.InputType = InputTypes.TextFlagNoSuggestions;

                Control.Background = gradient;
                Control.SetPadding(padding, padding, padding, padding);
            }
        }

        private void SetReturnType(RoundedCornerEntry entry)
        {
            var type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                case ReturnType.Default:
                    break;
                case ReturnType.Done:
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }
    }
}