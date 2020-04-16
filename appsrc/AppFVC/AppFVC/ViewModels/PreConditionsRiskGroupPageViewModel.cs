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
using AppFVCShared.Model;
using AppFVCShared.Services;
using AppFVCShared.WebService;
using FCVLibWS;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class PreConditionsRiskGroupPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        readonly IStoreService _storeService;

        private Client objClient;
        private ContactWs contactWs;

        public Command NavegarNext { get; set; }
        public Command SimCommand { get; set; }
        public Command NaoCommand { get; set; }

        #region Propriedades

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }


        private string _simColor;

        [Obsolete]
        public string SimColor
        {
            get
            {
                return _simColor;
            }
            set
            {
                if (_simColor != value)
                {
                    _simColor = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _naoColor;

        [Obsolete]
        public string NaoColor
        {
            get
            {
                return _naoColor;
            }
            set
            {
                if (_naoColor != value)
                {
                    _naoColor = value;
                    OnPropertyChanged();
                }
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

        private string _naoTextColor;
        public string NaoTextColor
        {
            get { return _naoTextColor; }
            set
            {
                SetProperty(ref _naoTextColor, value);
                RaisePropertyChanged("NaoTextColor");

            }
        }

        private string _simTextColor;
        public string SimTextColor
        {
            get { return _simTextColor; }
            set
            {
                SetProperty(ref _simTextColor, value);
                RaisePropertyChanged("SimTextColor");

            }
        }

        #endregion

        public PreConditionsRiskGroupPageViewModel(INavigationService navigationService, IStoreService storeService) : base(navigationService)
        {
            _storeService = storeService;
            IsBusy = false;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            SimCommand = new Command(() => SimCommandExecute());
            NaoCommand = new Command(() => NaoCommandExecute());

            SimColor = "#FAFAFA";
            NaoColor = "#FAFAFA";
            SimTextColor = "#219653";
            NaoTextColor = "#219653";
        }

        private void NaoCommandExecute()
        {
            NaoColor = "#219653";
            SimColor = "#FAFAFA";
            NaoTextColor = "#FAFAFA";
            SimTextColor = "#219653";
        }

        private void SimCommandExecute()
        {
            SimColor = "#219653";
            NaoColor = "#FAFAFA";
            NaoTextColor = "#219653";
            SimTextColor = "#FAFAFA";
        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            if (SimColor == "#219653")
            {
                AppUser.ConditionRiskGroup = true;
            }
            else if (NaoColor == "#219653")
            {
                AppUser.ConditionRiskGroup = false;
            }
            else
            {
                AppUser.ConditionRiskGroup = null;
            }

            AppUser.CreateRecord = DateTime.Now;
            SaveUser();
            var result = await RegisterUser();
            if (result != null)
            {
                await _navigationService.NavigateAsync("/StatusHealthyPage");
                Erro = "";
            }
            else
            {
                Erro = "Erro no cadastro";
            }
            IsBusy = false;
        }

        private async Task<Contact> RegisterUser()
        {
            objClient = new Client(Configuration.UrlBase);
            contactWs = new ContactWs(objClient);
            
            var result = await contactWs.RegisterContact(AppUser);
            var r = await contactWs.Contacts();
            return result;

        }

        private void SaveUser()
        {
            var users = _storeService.FindAll<User>();
            if (users != null || users.Count() != 0)
            {
                _storeService.RemoveAll<User>();
            }           
            _storeService.Store<User>(AppUser);

            users = _storeService.FindAll<User>();
        }
    }
}
