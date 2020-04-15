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
    public class StatusWebViewPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public Command NavigationPop { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                SetProperty(ref _url, value);
                RaisePropertyChanged("Url");

            }
        }

        public StatusWebViewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            NavigationPop = new Command(async () => await NavigationPopCommand());
            Url = "https://app.contracovid.com.br/newperson?nome=jean&celular=46991067240&idade=31&aceito=true&sexo=M&diabetes=true&hipertensao=true&pressaobaixa=true&problemasrespiratorios=true&doencaoncologica=true";

        }
        private async Task NavigationPopCommand()
        {
            if (Status == "Unknow")
            {
                await _navigationService.NavigateAsync("/StatusHealthyPage");
            }
            else if (Status == "Recovered")
            {
                await _navigationService.NavigateAsync("/StatusImunePage");
            }
            else if (Status == "Isolated")
            {
                await _navigationService.NavigateAsync("/StatusIsolationPage");
            }
            else if (Status == "Quarentined")
            {
                await _navigationService.NavigateAsync("/StatusQuarantinePage");
            }
        }
    }
}
