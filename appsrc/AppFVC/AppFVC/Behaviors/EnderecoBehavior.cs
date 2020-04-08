using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppFVC.Behaviors
{
    class EnderecoBehavior : Behavior<Entry>
    {
        const string nomeRegex = @"^((\b[A-zÀ-ú']{2,40}\b)\s*){1,}$";
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



        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //bool IsValid = false;
            //IsValid = (Regex.IsMatch(e.NewTextValue, nomeRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            //((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;

            var entry = (Entry)sender;

            entry.Text = FormatWord(entry.Text);
        }


        private string FormatWord(string input)
        {           
            var digitsRegex = new Regex(@"[^a-zA-ZáéíóúàèìòùâêîôûãõçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÇ0-9 ]");
            var digits = digitsRegex.Replace(input, "");
            if (digits == "")
                return digits;

            if (digits.Length < 2)
            {
                if (digits.Substring(0) == " ")
                    return "";
                return digits.ToUpper();

            }
            return digits;
        }
    }
}
