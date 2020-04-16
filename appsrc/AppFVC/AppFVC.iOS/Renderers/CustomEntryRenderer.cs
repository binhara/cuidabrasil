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
using AppFVC.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace AppFVC.iOS.Renderers
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                //Control.TextColor = UIColor.White;

            }
        }
    }
}