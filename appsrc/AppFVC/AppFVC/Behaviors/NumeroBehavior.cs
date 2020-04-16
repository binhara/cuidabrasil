//
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppFVC.Behaviors
{
    public class NumeroBehavior : Behavior<Entry>
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
            if (digits.Length >= 2)
            {
                if (digits.Substring(digits.Length - 1) == "." || digits.Substring(digits.Length - 1) == ",")
                    return $"{digits.Substring(0, digits.Length - 1)}";
                return digits;
            }

            return digits;
        }
    }
}
