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
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppFVC.Behaviors
{
    public class NomeBehavior : Behavior<Entry>
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
            bool IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, nomeRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            //((Entry)sender).TextColor = IsValid ? Color.Default : Color.Default;
            //if (((Entry)sender).TextColor == Color.FromHex("#EB5757"))
            //    ((Entry)sender).TextColor = Color.FromHex("#222222");

            var entry = (Entry)sender;

            entry.Text = FormatName(entry.Text);
        }


        private string FormatName(string input)
        {
            var digitsRegex = new Regex(@"[^a-zA-ZáéíóúàèìòùâêîôûãõçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÇ ]");
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

