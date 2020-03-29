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
    public class SmsPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarNext { get; set; }
        public SmsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavegarNext = new Command(async () => await NavegarNextCommand());
        }

        private async Task NavegarNextCommand()
        {
            _navigationService.NavigateAsync("PreConditionsPage");
        }
    }
}
