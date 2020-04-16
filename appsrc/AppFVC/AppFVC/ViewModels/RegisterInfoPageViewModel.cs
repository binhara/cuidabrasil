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
