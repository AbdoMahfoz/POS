using System;
using CoreGraphics;
using MobileApp.Controls;
using MobileApp.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedCornerEntry), typeof(RoundedEntryRenderer))]

namespace MobileApp.iOS.CustomRenderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var roundedView = Element as RoundedCornerEntry;

                if (roundedView != null)
                {
                    NativeView.Layer.CornerRadius =(nfloat)roundedView.BorderRadius;
                    NativeView.Layer.BorderWidth = roundedView.BorderThickness;
                    NativeView.Layer.BorderColor = roundedView.BorderColor.ToCGColor();
                    NativeView.Layer.BackgroundColor = roundedView.EntryBackgroundColor.ToCGColor();
                }

                if (roundedView != null && !roundedView.DisplaySuggestions)
                {
                    Control.AutocorrectionType = UITextAutocorrectionType.No;

                    SetReturnType(roundedView);

                    Control.ShouldReturn += tf =>
                    {
                        roundedView.InvokeCompleted();
                        return true;
                    };
                }

                Control.LeftView = new UIView(new CGRect(0, 0, 15, Control.Frame.Height));
                Control.RightView = new UIView(new CGRect(0, 0, 15, Control.Frame.Height));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.BorderStyle = UITextBorderStyle.None;

                UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
                {
                    if (Element != null)
                    {
                        //   Element.Margin = new Thickness(0, 0, 0, args.FrameEnd.Height); //push the entry up to keyboard height when keyboard is activated
                    }
                });

                UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
                {
                    if (Element != null)
                    {
                        //Element.Margin = new Thickness(0); //set the margins to zero when keyboard is dismissed
                    }
                });
            }
        }


        private void SetReturnType(RoundedCornerEntry entry)
        {
            var type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case ReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case ReturnType.Send:
                    Control.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case ReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case ReturnType.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                case ReturnType.Default:
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }
    }
}