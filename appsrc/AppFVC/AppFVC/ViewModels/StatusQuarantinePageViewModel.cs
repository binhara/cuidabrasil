using AppFVCShared.Model;
using AppFVCShared.Services;
using AppFVCShared.Teste;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class StatusQuarantinePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        readonly IStoreService _storeService;

        public News NewsSelect { get; set; }

        private ObservableCollection<News> _NewsItems;
        public ObservableCollection<News> NewsItems
        {
            get { return _NewsItems; }
            set { SetProperty(ref _NewsItems, value); }
        }

        public Command NavegarPaginaHealthy { get; set; }
        public Command VisualizarMapa { get; set; }
        public Command NavigateUrlOrPhoneNumber { get; set; }

        #region Propriedades

        private string _headerTitle;
        public string HeaderTitle
        {
            get { return _headerTitle; }
            set
            {
                SetProperty(ref _headerTitle, value);
                RaisePropertyChanged("HeaderTitle");

            }
        }

        private string _headerBody;
        public string HeaderBody
        {
            get
            {
                return _headerBody;
            }

            set
            {
                SetProperty(ref _headerBody, value);
                RaisePropertyChanged("HeaderBody");
            }
        }

        #endregion

        public StatusQuarantinePageViewModel(INavigationService navigationService, IStoreService storeService) :base(navigationService)
        {
            _storeService = storeService;
            NewsItems = new ObservableCollection<News>();
            _navigationService = navigationService;
            NavegarPaginaHealthy = new Command(async () => await NavegarPaginaCommand());
            VisualizarMapa = new Command(async () => await VisualizarMapaCommand());
            NavigateUrlOrPhoneNumber = new Command<News>(async (obj) => await ExecuteNavigateUrlOrPhoneNumber(obj));

            GetNewsData();
        }

        public async void GetNewsData()
        {
            var users = _storeService.FindAll<User>();
            var user = users.ToList()[0];
            var telefone = user.DddPhoneNumber;
            var ddd = telefone.Substring(0, 2);
            NewsWr newsWr = new NewsWr();
            var result = newsWr.GetJsonData(ddd, "Quarentined");
            if (result != null)
            {
                NewsItems = new ObservableCollection<News>(result.news);
                HeaderTitle = result.header_title;
                HeaderBody = result.header_body;
            }

        }

        private async Task VisualizarMapaCommand()
        {
            await _navigationService.NavigateAsync("/StatusWebView");
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
