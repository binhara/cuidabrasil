using Prism.Navigation;
using System;
using System.Linq;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
<<<<<<< HEAD
=======
using AppFVCShared.Model;
>>>>>>> Develop
using AppFVCShared.Services;
using AppFVCShared.WebRequest;

namespace AppFVC.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {
        private readonly ICacheService _cacheService;

        private readonly INavigationService _navigationService;
        readonly IStoreService _storeService;
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
        public WelcomePageViewModel(INavigationService navigationService, IStoreService storeService) : base(navigationService)
        {
            _navigationService = navigationService;
            _storeService = storeService;

            
            GeoLocationCommand = new Command(async () => await GeolocationCommand());
            AppUser = new AppFVCShared.Model.User();
            IsBusy = false;

           
            //Preferences.Remove("Date");
            //Preferences.Clear();
            SaveData();

            // verificar se ja tem dados no banco
            var datauser = _storeService.FindAll<User>();

            // Se tiver .. deve carregar os dados do usuario.

            if(datauser.Any())
            {
                NavegarNext = new Command(async () => await NavegarStatusPage());
            }
            else
            {
                NavegarNext = new Command(async () => await NavegarNextCommand());
            }

            GetFirstRunData();
        }

        public void GetFirstRunData()
        {
            string ddd;
            var users = _storeService.FindAll<User>();
            if (users.Any())
            {
                var user = users.ToList()[0];
                var telefone = user.DddPhoneNumber;
                ddd = telefone.Substring(0, 2);
            }
            else
            {
                ddd = "00";
            }
            FirstRunWr news = new FirstRunWr();
            var result = news.GetJsonFirstRunData(ddd);
            if (result != null)
            {
                Welcome_title = result.Welcome_title;
                Welcome_body = result.Welcome_body;
                Welcome_end = result.Welcome_end;
                Welcome_bold = result.Welcome_bold;
            }
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
        private async Task NavegarStatusPage()
        {
            IsBusy = true;
            await _navigationService.NavigateAsync("/StatusHealthyPage");
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
