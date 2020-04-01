using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppFVC.ViewModels
{
    public class PreConditionsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarNext { get; set; }
        public PreConditionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
        }

        private async Task NavegarNextCommand()
        {
            _navigationService.NavigateAsync("/StatusHealthyPage");
        }
    }
}
