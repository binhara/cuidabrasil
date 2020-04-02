using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppFVC.Behaviors
{
    public class IdadeBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;

            base.OnDetachingFrom(bindable);
        }

        void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            bool IsValid = false;
            IsValid = args.NewTextValue.Length <= 3;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;

            var entry = (Entry)sender;

            entry.Text = FormatPhoneNumber(entry.Text);
        }

        private string FormatPhoneNumber(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input, "");
            if (digits == "")
                return digits;

            if (digits.Length < 2)
            {
                if (digits.Substring(0) == "." || digits.Substring(0) == "," || digits.Substring(0) == "0")
                    return "";
                return digits;
            }
            if (digits.Length < 3)
            {
                if (digits.Substring(1) == "." || digits.Substring(1) == ",")
                    return $"{digits.Substring(0, 1)}";
                return digits;
            }

            if (digits.Length < 4)
            {
                if (digits.Substring(2) == "." || digits.Substring(2) == ",")
                    return $"{digits.Substring(0, 2)}";
                return digits;
            }

            return digits;
        }
    }
}