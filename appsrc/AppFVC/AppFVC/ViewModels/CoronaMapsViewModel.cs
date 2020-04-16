//
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//      Adriano D'Luca Binhara Gonçalves (adriano@azuris.com.br)
//  	Carol Yasue (carolina_myasue@hotmail.com)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//

using Prism.Navigation;
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
