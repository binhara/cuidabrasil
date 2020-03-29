using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AppFVC;
using AppFVC.CustomRenders;
using AppFVC.Droid;
using AppFVC.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace AppFVC.Droid.Renderers
{
    [Obsolete]
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                //gd.SetCornerRadius(10); // increase or decrease to changes the corner look            
                this.Control.SetBackgroundDrawable(gd);
                //this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);


                //Control.Background = Resources.GetDrawable(Resource.Drawable.RoundedEntry);
                //Control.SetPadding(1, 3, 1, 1);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}