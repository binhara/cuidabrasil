using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class TermsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavigationPop { get; set; }
        public TermsPageViewModel(INavigationService navigationService) :base(navigationService)
        {
            _navigationService = navigationService;

            NavigationPop = new Command(async () => await NavigationPopCommand());
        }
        private async Task NavigationPopCommand()
        {
            await _navigationService.NavigateAsync("RegisterPage");
        }
    }
}
