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
    public class RegisterPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarNext { get; set; }
        public Command NavegarRegisterInfo { get; set; }
        public RegisterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            NavegarRegisterInfo = new Command(async () => await NavegarRegisterInfoCommand());
        }

        private async Task NavegarRegisterInfoCommand()
        {
            _navigationService.NavigateAsync("/RegisterInfoPage");
        }

        private  async Task NavegarNextCommand()
        {
            _navigationService.NavigateAsync("/SmsPage");
        }
    }
}
