using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFVC.Views
{
    public partial class GeoLocationPage : ContentPage
    {
        public GeoLocationPage()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location == null)
                    LabelLocation.Text = "Sem GPS";
                LabelLocation.Text = $"{location.Latitude} {location.Longitude}";
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"Erro na localização: {ex.Message}");
            }
        }
    }
}
