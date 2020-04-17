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
    public class RiskGroupPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarBack { get; set; }
        public RiskGroupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            NavegarBack = new Command(async () => await NavegarBackCommand());
        }
        private async Task NavegarBackCommand()
        {
            await _navigationService.NavigateAsync("PreConditionsRiskGroupPage");
        }
    }
}
