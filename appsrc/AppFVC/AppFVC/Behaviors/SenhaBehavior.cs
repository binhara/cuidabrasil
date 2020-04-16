//
//
// Author:
//      Adriano D'Luca Binhara Gonçalves (adriano@azuris.com.br)
//  	Carol Yasue (carolina_myasue@hotmail.com)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using Xamarin.Forms;

namespace AppFVC.Behaviors
{
    public class SenhaBehavior : Behavior<Entry>
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



        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = e.NewTextValue.Length > 4;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
    }
}
