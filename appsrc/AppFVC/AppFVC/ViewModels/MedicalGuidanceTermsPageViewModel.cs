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
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class MedicalGuidanceTermsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public Command NavigateNext { get; set; }
        public Command NavigationPop { get; set; }

        #region Propriedades

        public string Terms { get; set; }

        private string _buttonColor;
        public string ButtonColor
        {
            get
            {
                return _buttonColor;
            }
            set
            {
                SetProperty(ref _buttonColor, value);
                RaisePropertyChanged("ButtonColor");
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }

        bool _checkTermo;
        public bool CheckTermo
        {
            get
            {
                return _checkTermo;
            }
            set
            {
                _checkTermo = value;

                if (_checkTermo)
                {
                    Erro = "";
                    IVErro = false;
                }
                // Do any other stuff you want here
            }
        }

        private string _erro;
        public string Erro
        {
            get { return _erro; }
            set
            {
                SetProperty(ref _erro, value);
                RaisePropertyChanged("Erro");

            }
        }

        private bool _iVErro;
        public bool IVErro
        {
            get { return _iVErro; }
            set
            {
                { SetProperty(ref _iVErro, value); }
            }
        }

        #endregion
        public MedicalGuidanceTermsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            NavigateNext = new Command(async () => await NavigateNextCommand());
            NavigationPop = new Command(async () => await NavigationPopCommand());

            GetTerms();
        }

        private void GetTerms()
        {
            var telefone = AppUser.DddPhoneNumber;
            var ddd = telefone.Substring(0, 2);
            TermsMedicalGuidanceWr news = new TermsMedicalGuidanceWr();
            var result = news.GetJsonData(ddd);
            if (result != null)
            {
                Terms = result.Terms;
            }
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

        private async Task NavigateNextCommand()
        {
            IsBusy = true;

            if (_checkTermo == false)
            {
                IVErro = true;
                Erro = "Você precisa aceitar os termos de uso para prosseguir.";
                IsBusy = false;
            }
            else
            {
                await _navigationService.NavigateAsync("StatusWebViewPage");
                IsBusy = false;
                Erro = "";
            }
            IsBusy = false;


        }
    }
}
