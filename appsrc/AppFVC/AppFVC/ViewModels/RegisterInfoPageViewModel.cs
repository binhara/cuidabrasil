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
    public class RegisterInfoPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public Command NavigationPop { get; set; }
        public RegisterInfoPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigationPop = new Command(async () => await NavigationPopCommand());
        }

        private async Task NavigationPopCommand()
        {
            _navigationService.NavigateAsync("RegisterPage");
        }
    }
}
