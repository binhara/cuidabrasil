using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class CoronaMapsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavigationPop { get; set; }
        public CoronaMapsViewModel(INavigationService navigationService) : base(navigationService)
        {

            _navigationService = navigationService;

            NavigationPop = new Command(async () => await NavigationPopCommand());
        }
        private async Task NavigationPopCommand()
        {
            _navigationService.NavigateAsync("/StatusHealthyPage");
        }
    }
}
