using Prism.Navigation;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using AppFVCShared.WebRequest;

namespace AppFVC.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {
        private readonly ICacheService _cacheService;

        private readonly INavigationService _navigationService;
        public ICommand NavegarNext { get; set; }
        public string Welcome_title { get; set; }
        public string Welcome_body { get; set; }
        public string Welcome_end { get; set; }
        public string Welcome_bold { get; set; }
        public ICommand GeoLocationCommand { get; }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }

        //private static ICacheService cacheService;
        //private static ObservableCollection<User> _users;
        public WelcomePageViewModel(INavigationService navigationService) : base(navigationService)        
        {
            FirstRunWr news = new FirstRunWr();
            var result = news.GetJsonFirstRunData("00");
            
            _navigationService = navigationService;
            NavegarNext = new Command(async() =>await NavegarNextCommand());
            GeoLocationCommand = new Command(async () => await GeolocationCommand());
            AppUser = new AppFVCShared.Model.User();
            IsBusy = false;
            Welcome_title = result.Welcome_title;
            Welcome_body = result.Welcome_body;
            Welcome_end = result.Welcome_end;
            Welcome_bold = result.Welcome_bold;

            //Preferences.Remove("Date");
            //Preferences.Clear();
            SaveData();
        }

        private async Task SaveData() {

            var date = await SecureStorage.GetAsync("Date");
            var hour = await SecureStorage.GetAsync("Hour");
            var lat = await SecureStorage.GetAsync("Latitude");
            var lon = await SecureStorage.GetAsync("Longitude");
            var alt = await SecureStorage.GetAsync("Altitude");

            if (date == null || date == "")
            {
                try
                {
                    await SecureStorage.SetAsync("Date", DateTime.Today.Date.ToString());
                    await SecureStorage.SetAsync("Hour", DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString());


                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {

                        await SecureStorage.SetAsync("Latitude", location.Latitude.ToString());
                        await SecureStorage.SetAsync("Longitude", location.Longitude.ToString());
                        await SecureStorage.SetAsync("Altitude", location.Altitude.ToString());

                        //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
            }
        }


        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            await _navigationService.NavigateAsync("/RegisterPage");
        }
        private async Task GeolocationCommand()
        {
            IsBusy = true;
            await _navigationService.NavigateAsync("GeoLocationPage");
        }


    }
}
