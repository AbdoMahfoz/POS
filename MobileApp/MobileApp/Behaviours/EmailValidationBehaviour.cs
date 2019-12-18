using MobileApp.Helpers;
using Xamarin.Forms;

namespace MobileApp.Behaviours
{
    public class EmailValidationBehaviour : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var email = e.NewTextValue;
            var entry = sender as Entry;
            if (string.IsNullOrEmpty(email) || entry == null) return;
            entry.TextColor = EmailValidator.IsEmailValid(email) ? Color.DarkSeaGreen : Color.Crimson;
        }
    }
}