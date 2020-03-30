using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Collections.ObjectModel;

namespace AppFVC.ViewModels
{
    public class GeoLocationViewModel : BindableBase
    {

        #region Propriedades

        int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        string _statusTracking;
        public string StatusTracking
        {
            get => _statusTracking;
            set => SetProperty(ref _statusTracking, value);
        }

        string _countUpdate;
        public string CountUpdate
        {
            get => _countUpdate;
            set => SetProperty(ref _countUpdate, value);
        }

        string _btnTrack;
        public string ButtonTrack
        {
            get => _btnTrack;
            set => SetProperty(ref _btnTrack, value);
        }



        bool _tracking;
        public bool Tracking
        {
            get => _tracking;
            set => SetProperty(ref _tracking, value);
        }

        ObservableCollection<Position> _positions;
        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }
        #endregion

        public ICommand TrackingLocationCommand { get; }

        public GeoLocationViewModel()
        {
            TrackingLocationCommand = new Command(async () => await ExecuteTrakingLocationCommandAsync());
            ButtonTrack = "Track Movement";
            Positions = new ObservableCollection<Position>();

        }

        async Task ExecuteTrakingLocationCommandAsync()
        {
            try
            {
                if (!CrossGeolocator.IsSupported)
                    return;

                if (Tracking)
                {
                    CrossGeolocator.Current.PositionChanged -= CrossGeolocator_Current_PositionChanged;
                    CrossGeolocator.Current.PositionError -= CrossGeolocator_Current_PositionError;
                }
                else
                {
                    CrossGeolocator.Current.PositionChanged += CrossGeolocator_Current_PositionChanged;
                    CrossGeolocator.Current.PositionError += CrossGeolocator_Current_PositionError;
                }

                if (CrossGeolocator.Current.IsListening)
                {
                    await CrossGeolocator.Current.StopListeningAsync();
                    StatusTracking = "Stopped tracking";
                    ButtonTrack = "Start Tracking";
                    Tracking = false;
                    Count = 0;
                }
                else
                {
                    Positions.Clear();
                    if (await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10),
                        10, false, new ListenerSettings {

                            ActivityType = ActivityType.OtherNavigation,
                            AllowBackgroundUpdates = true,
                            DeferLocationUpdates = true,
                            DeferralDistanceMeters = 1,
                            DeferralTime = TimeSpan.FromSeconds(1),
                            ListenForSignificantChanges = true,
                            PauseLocationUpdatesAutomatically = false
                        }))
                    {
                        StatusTracking = "Started tracking";
                        ButtonTrack = "Stop Tracking";
                        Tracking = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro aqui: {ex.Message}");
                //await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
            }
        }

        void CrossGeolocator_Current_PositionError(object sender, PositionErrorEventArgs e)
        {

            StatusTracking = "Location error: " + e.Error.ToString();
        }

        void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var position = e.Position;
                Positions.Add(position);
                Count++;
                CountUpdate = $"{Count} updates";
                StatusTracking = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nSpeed: {6}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Speed);

            });
        }
    }



}
