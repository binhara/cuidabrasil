using AppFVCShared.WebRequest;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class RegisterInfoPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavigationPop { get; set; }

        public string Why_title { get; set; }
        public string Why_Body { get; set; }
        public string Why_middle { get; set; }
        public string Why_end { get; set; }
        public RegisterInfoPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            FirstRunWr news = new FirstRunWr();
            var result = news.GetJsonFirstRunData("00");
            _navigationService = navigationService;

            NavigationPop = new Command(async () => await NavigationPopCommand());
            Why_title = result.Why_title;
            Why_Body = result.Why_Body;
            Why_middle = result.Why_middle;
            Why_end = result.Why_end;

        }

        private async Task NavigationPopCommand()
        {
            _navigationService.NavigateAsync("RegisterPage");
        }
    }
}
