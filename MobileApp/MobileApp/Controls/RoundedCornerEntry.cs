using System;
using MobileApp.Behaviours;
using Xamarin.Forms;

namespace MobileApp.Controls
{
    public class RoundedCornerEntry : Entry
    {
        public static readonly BindableProperty DisplaySuggestionsProperty =
            BindableProperty.Create(nameof(DisplaySuggestions), typeof(bool), typeof(RoundedCornerEntry), true);

        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(RoundedCornerEntry), 0);

        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(double), typeof(RoundedCornerEntry), 0.0);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundedCornerEntry), Color.Default);

        public static readonly BindableProperty EntryBackgroundColorProperty =
            BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(RoundedCornerEntry), Color.Default);

        public static readonly BindableProperty IsEmailProperty =
            BindableProperty.Create(nameof(IsEmail), typeof(bool), typeof(RoundedCornerEntry), false,
                BindingMode.TwoWay,
                propertyChanged: SetBehaviour);

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
            nameof(ReturnType),
            typeof(ReturnType),
            typeof(RoundedCornerEntry),
            ReturnType.Done
        );

        public bool DisplaySuggestions
        {
            get => (bool) GetValue(DisplaySuggestionsProperty);
            set => SetValue(DisplaySuggestionsProperty, value);
        }

        public int BorderThickness
        {
            get => (int) GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public double BorderRadius
        {
            get => (double) GetValue(BorderRadiusProperty);
            set => SetValue(BorderRadiusProperty, value);
        }

        public Color BorderColor
        {
            get => (Color) GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public bool IsEmail
        {
            get => (bool) GetValue(IsEmailProperty);
            set => SetValue(IsEmailProperty, value);
        }

        public Color EntryBackgroundColor
        {
            get => (Color) GetValue(EntryBackgroundColorProperty);
            set => SetValue(EntryBackgroundColorProperty, value);
        }

        public ReturnType ReturnType
        {
            get => (ReturnType) GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        private static void SetBehaviour(BindableObject bindable, object oldvalue, object newvalue)
        {
            var email = (bool) newvalue;
            var entry = (RoundedCornerEntry) bindable;
            if (email)
                entry.Behaviors.Add(new EmailValidationBehaviour());
        }

        public new event EventHandler Completed;

        public void InvokeCompleted()
        {
            Completed?.Invoke(this, null);
        }
    }
}