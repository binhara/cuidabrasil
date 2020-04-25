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
using AppFVCShared.Services;
using AppFVCShared.Validators;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        readonly IStoreService _storeService;

        public Command NavegarNext { get; set; }
        public Command NavegarRegisterInfo { get; set; }
        public Command NavegarTerms { get; set; }

        #region IsBusy

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }

        #endregion

        #region CheckBox

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

        bool _checkPref;
        public bool CheckPref
        {
            get
            {
                return _checkPref;
            }
            set
            {
                _checkPref = value;

                if (_checkPref)
                {
                    Erro = "";
                    IVErro = false;
                }
                // Do any other stuff you want here
            }
        }

        #endregion

        #region IsVisibleErro

        private bool _iVErro;
        public bool IVErro
        {
            get { return _iVErro; }
            set
            {
                { SetProperty(ref _iVErro, value); }
            }
        }

        private bool _ivnumero;
        public bool IVNumero
        {
            get
            {
                return _ivnumero;
            }

            set
            {
                //ValidadorTelefone();
                SetProperty(ref _ivnumero, value);
                RaisePropertyChanged("IVNumero");
            }
        }

        private bool _ivIdade;
        public bool IVIdade
        {
            get
            {
                return _ivIdade;
            }

            set
            {
                SetProperty(ref _ivIdade, value);
                RaisePropertyChanged("IVIdade");
            }
        }

        private bool _iVNome;
        public bool IVNome
        {
            get { return _iVNome; }
            set
            {
                { SetProperty(ref _iVNome, value); }
            }
        }

        #endregion

        #region Erro

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

        private string _erroNumero;
        public string ErroNumero
        {
            get
            {
                return _erroNumero;
            }

            set
            {
                SetProperty(ref _erroNumero, value);
                RaisePropertyChanged("ErroNumero");
            }
        }

        private string _erroIdade;
        public string ErroIdade
        {
            get
            {
                return _erroIdade;
            }

            set
            {
                SetProperty(ref _erroIdade, value);
                RaisePropertyChanged("ErroIdade");
            }
        }

        private string _erroNome;
        public string ErroNome
        {
            get { return _erroNome; }
            set
            {
                SetProperty(ref _erroNome, value);
                RaisePropertyChanged("ErroNome");

            }
        }

        #endregion

        #region TxtColor & ButtonColor

        private string _txtColorNome;
        public string TxtColorNome
        {
            get { return _txtColorNome; }
            set
            {
                if (_txtColorNome != value)
                {
                    _txtColorNome = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _txtColorPhone;
        public string TxtColorPhone
        {
            get
            {
                return _txtColorPhone;
            }
            set
            {
                SetProperty(ref _txtColorPhone, value);
                RaisePropertyChanged("TxtColorPhone");
            }
        }

        private string _txtColorIdade;
        public string TxtColorIdade
        {
            get
            {
                return _txtColorIdade;
            }
            set
            {
                SetProperty(ref _txtColorIdade, value);
                RaisePropertyChanged("TxtColorIdade");
            }
        }

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

        #endregion

        #region Campos

        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                SetProperty(ref _nome, value);
                RaisePropertyChanged("Nome");
                if (TxtColorNome != "#222222")
                {
                    TxtColorNome = "#222222";
                }
                if (_nome != "")
                {
                    NomePreenchido = true;
                    MudarCorBotao();
                }
                else
                {
                    NomePreenchido = false;
                    ButtonColor = "#BDBDBD";
                }
            }
        }

        private string _idade;
        public string Idade
        {
            get
            {
                return _idade;
            }
            set
            {
                SetProperty(ref _idade, value);
                RaisePropertyChanged("Idade");

                if (_idade != "")
                {
                    IdadePreenchido = true;
                    MudarCorBotao();
                }
                else
                {
                    IdadePreenchido = false;
                    ButtonColor = "#BDBDBD";
                }
            }
        }

        private string _telefone;
        public string NumeroTelefone
        {
            get
            {
                return _telefone;
            }

            set
            {
                SetProperty(ref _telefone, value);
                RaisePropertyChanged("Telefone");
                if (TxtColorPhone != "#222222")
                {
                    TxtColorPhone = "#222222";
                }
                if (_telefone != "")
                {
                    PhonePreenchido = true;
                    MudarCorBotao();
                }
                else
                {
                    PhonePreenchido = false;
                    ButtonColor = "#BDBDBD";
                }
            }
        }

        #endregion

        #region Boolean campos preenchidos
        bool NomePreenchido;

        bool PhonePreenchido;

        bool IdadePreenchido;
        #endregion

        public void MudarCorBotao()
        {
            if (NomePreenchido && PhonePreenchido && IdadePreenchido)
            {
                ButtonColor = "#219653";
            }
        }

        public bool ValidadorTelefone()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_telefone);
            if (result == true)
            {
                var val = new CellphoneValidator<string>();
                result = val.Check(_telefone);
                ErroNumero = val.ValidationMessage;
            }
            else
            {
                TxtColorPhone = "#EB5757";
                ErroNumero = "Por favor, informe seu telefone.";
            }
            if (result == false)
            {
                TxtColorPhone = "#EB5757";
                IVNumero = true;
            }
            else
            {
                TxtColorPhone = "#222222";
                IVNumero = false;
            }
            return result;
        }

        public bool ValidadorIdade()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_idade);
            if (result == false)
            {
                TxtColorIdade = "#EB5757";
                ErroIdade = "Por favor, informe sua idade.";
                IVIdade = true;
            }
            else
            {
                TxtColorIdade = "#222222";
                IVIdade = false;
            }
            return result;
        }

        public bool ValidadorNome()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_nome);
            if (result == true)
            {
                var val = new NameValidator<string>();
                result = val.Check(_nome);
                ErroNome = val.ValidationMessage;
            }
            else
            {
                TxtColorNome = "#EB5757";
                ErroNome = "Por favor, informe seu nome.";
            }
            if (result == false)
            {
                TxtColorNome = "#EB5757";
                IVNome = true;
            }
            else
            {
                TxtColorNome = "#222222";
                IVNome = false;
            }
            return result;
        }

        public RegisterPageViewModel(INavigationService navigationService, IStoreService storeService) : base(navigationService, storeService)
        {
            _storeService = storeService;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            NavegarRegisterInfo = new Command(async () => await NavegarRegisterInfoCommand());
            NavegarTerms = new Command(async () => await NavegarTermsCommand());

            #region Binding Componentes
            ButtonColor = "#BDBDBD";
            IsBusy = false;
            TxtColorNome = "#222222";
            TxtColorPhone = "#222222";
            TxtColorIdade = "#222222";
            Erro = "";
            ErroNome = "";
            ErroNumero = "";
            ErroIdade = "";
            IVNome = false;
            IVNumero = false;
            IVIdade = false;
            IVErro = false;
            Nome = AppUser.Name;
            NumeroTelefone = AppUser.DddPhoneNumber;
            Idade = AppUser.Age.ToString();
            #endregion
        }

        public void AdjustData()
        {
            if (Nome != "" && Nome != null)
            {
                Nome = Nome.TrimStart();
                Nome = Nome.TrimEnd();
            }
            AppUser.Name = Nome;
            if (NumeroTelefone != "" && NumeroTelefone != null)
            {
                AppUser.DddPhoneNumber = NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
            }
            if (Idade != "" && Idade != null)
            {
                AppUser.Age = Int32.Parse(Idade);
            }
        }

        private async Task NavegarTermsCommand()
        {
            IsBusy = true;
            AdjustData();
            var Url = new Uri("https://www.cuidabrasil.org/termos-de-uso");
            Device.OpenUri(Url);
            //await _navigationService.NavigateAsync("TermsPage");
            IsBusy = false;
        }

        private async Task NavegarRegisterInfoCommand()
        {
            IsBusy = true;
            AdjustData();
            await _navigationService.NavigateAsync("RegisterInfoPage");
            IsBusy = false;
        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            ValidadorNome();
            ValidadorTelefone();
            ValidadorIdade();
            if (ValidadorNome() == true && ValidadorTelefone() == true && ValidadorIdade() == true)
            {
                if (_checkTermo == false)
                {
                    IVErro = true;
                    Erro = "Você precisa aceitar os termos de uso para prosseguir.";
                    IsBusy = false;
                }
                else
                {
                    AdjustData();
                    AppUser.AcceptTerms = CheckTermo;

                    //await _navigationService.NavigateAsync("/SmsPage");
                    await _navigationService.NavigateAsync("/PreConditionsPage");

                    IsBusy = false;
                    Erro = "";
                }
                IsBusy = false;

                TxtColorNome = "#222222";
                TxtColorPhone = "#222222";
                TxtColorIdade = "#222222";
                ErroNome = "";
                ErroNumero = "";
                ErroIdade = "";
            }
            else
            {
                ValidadorNome();
                ValidadorTelefone();
                ValidadorIdade();
                IsBusy = false;
            }

        }
    }
}
