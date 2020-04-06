using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppFVC.Behaviors
{
    public class PhoneNumberFormatterBehavior : Behavior<Entry>
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
            IsValid = args.NewTextValue.Length == 15;
            //if(((Entry)sender).TextColor == Color.FromHex("#EB5757"))
            //    ((Entry)sender).TextColor = Color.FromHex("#222222");


            var entry = (Entry)sender;

            entry.Text = FormatPhoneNumber(entry.Text);
        }

        private string FormatPhoneNumber(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input, "");
            if (digits == "")
                return digits;

            if (digits.Length <= 2)
            {
                var num = Convert.ToInt32(digits);
                if (digits.Length > 1) 
                    if ((num < 11 || num > 99) || digits == "00" )
                        return "";

                return digits;

            }
            if(digits.Length <= 3)
            {
                if(digits.Substring(2) != "9")
                    return $"({digits.Substring(0, 2)})";
                return $"({digits.Substring(0, 2)}) {digits.Substring(2)}";
            }
            if (digits.Length <= 4)
            {
                if (digits.Substring(3) == "0")
                    return $"({digits.Substring(0, 2)}) {digits.Substring(2, 1)}";
                return $"({digits.Substring(0, 2)}) {digits.Substring(2)}";
            }

            if(digits.Length < 8)
            {
                var x = $"({digits.Substring(0, 2)}) {digits.Substring(2)}";
                return x;
            }

            if(digits.Length > 8)
            {
                var x = $"({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7)}";
                return x;
            }


            return $"({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7)}";
        }
    }
}
