using AppFVCShared.Model;
using AppFVCShared.Teste;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class StatusQuarantinePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public News NewsSelect { get; set; }

        private ObservableCollection<News> _NewsItems;
        public ObservableCollection<News> NewsItems
        {
            get { return _NewsItems; }
            set { SetProperty(ref _NewsItems, value); }
        }

        public Command NavegarPaginaHealthy { get; set; }
        public Command TermsOfUse { get; set; }
        public Command NavigateUrlOrPhoneNumber { get; set; }
        public StatusQuarantinePageViewModel(INavigationService navigationService) :base(navigationService)
        {
            NewsItems = new ObservableCollection<News>();
            _navigationService = navigationService;
            NavegarPaginaHealthy = new Command(async () => await NavegarPaginaCommand());
            TermsOfUse = new Command(async () => await TermsOfUseCommand());
            NavigateUrlOrPhoneNumber = new Command<News>(async (obj) => await ExecuteNavigateUrlOrPhoneNumber(obj));

            GetNewsData();
        }

        public async void GetNewsData()
        {
            NewsWr newsWr = new NewsWr();
            var result = newsWr.GetJsonData("41", "Quarentined");
            if (result != null)
            {
                NewsItems = new ObservableCollection<News>(result);
            }

        }

        private async Task TermsOfUseCommand()
        {
            await _navigationService.NavigateAsync("/TermsOfUse");
        }

        private async Task ExecuteNavigateUrlOrPhoneNumber(News obj)
        {
            NewsSelect = obj;

            if (NewsSelect.Uri.Contains("http"))
            {
                var Url = new Uri(NewsSelect.Uri);
                Device.OpenUri(Url);
            }
            else
            {
                PhoneDialer.Open(NewsSelect.PhoneNumber);
            }
        }

        private async Task NavegarPaginaCommand()
        {
            await _navigationService.NavigateAsync("/StatusImunePage");
        }

    }

}
