//
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//      Andre Xavier (Andre@azuris.com.br)
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
    class CepBehavior : Behavior<Entry>
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

            entry.Text = FormatCep(entry.Text);
        }

        private string FormatCep(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input, "");

            if (digits.Length <= 2)
            {
                return digits;
            }
            if (digits.Length < 6)
            {
                return $"{digits.Substring(0, 2)}.{digits.Substring(2)}";
            }

            if (digits.Length < 8)
            {
                return $"{digits.Substring(0, 2)}.{digits.Substring(2, 3)}-{digits.Substring(5)}";
            }
            if (digits == "00000000")
            {
                return "";
            }

            return $"{digits.Substring(0, 2)}.{digits.Substring(2, 3)}-{digits.Substring(5, 3)}";
        }
    }
}

