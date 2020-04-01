using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppFVC.ViewModels
{
    public class PreConditionsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarNext { get; set; }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }
        public PreConditionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsBusy = false;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            _navigationService.NavigateAsync("/StatusHealthyPage");
        }
    }
}
