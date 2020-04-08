using AppFVCShared.Model;
using AppFVCShared.Validators;
using AppFVCShared.WebService;
using FCVLibWS;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private Client objClient;
        private ContactWs contactWs;
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
                if(_nome != "")
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
            if(NomePreenchido && PhonePreenchido && IdadePreenchido)
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

        public RegisterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
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

        private async Task NavegarTermsCommand()
        {
            IsBusy = true;
            if (Nome != "" && Nome != null)
            {
                Nome = Nome.TrimStart();
                Nome = Nome.TrimEnd();
            }
            AppUser.Name = Nome;
            AppUser.DddPhoneNumber = NumeroTelefone;
            if (Idade != "")
            {
                AppUser.Age = Int32.Parse(Idade);
            }
            await _navigationService.NavigateAsync("TermsPage");
            IsBusy = false;
        }

        private async Task NavegarRegisterInfoCommand()
        {
            IsBusy = true;
            if (Nome != "" && Nome != null)
            {
                Nome = Nome.TrimStart();
                Nome = Nome.TrimEnd();
            }
            AppUser.Name = Nome;
            AppUser.DddPhoneNumber = NumeroTelefone;
            if (Idade != "")
            {
                AppUser.Age = Int32.Parse(Idade);
            }
            await _navigationService.NavigateAsync("RegisterInfoPage");
            IsBusy = false;
        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            ValidadorNome();
            ValidadorTelefone();
            if (Idade == null || Idade == "")
            {
                IVIdade = true;
                TxtColorIdade = "#EB5757";
                ErroIdade = "Por favor, informe sua idade.";
                IsBusy = false;
            }
            else
            {
                TxtColorIdade = "#222222";
                IVIdade = false;
                ErroIdade = "";
                IsBusy = false;
            }
            if (ValidadorNome() == true && ValidadorTelefone() == true && Idade != null && Idade != "")
            {
                //NovoUsuario.Nome = Nome;
                if (_checkTermo == false)
                {
                    IVErro = true;
                    Erro = "Você precisa aceitar os termos de uso para prosseguir.";
                    IsBusy = false;
                }
                else
                {
                    Nome = Nome.TrimStart();
                    Nome = Nome.TrimEnd();

                    RegisterUser();
                    AppUser.Name = Nome;
                    AppUser.DddPhoneNumber = NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "")
                                               .Replace("-", "");
                    AppUser.Age = Int32.Parse(Idade);



                    await _navigationService.NavigateAsync("/SmsPage");
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
                if (Idade == null || Idade == "")
                {
                    IVIdade = true;
                    TxtColorIdade = "#EB5757";
                    ErroIdade = "Por favor, informe sua idade.";
                    IsBusy = false;
                }
                else
                {
                    TxtColorIdade = "#222222";
                    IVIdade = false;
                    ErroIdade = "";
                    IsBusy = false;
                }
                IsBusy = false;
            }

        }

        private async void RegisterUser()
        {
            User user = new User();

            objClient = new Client(Configuration.UrlBase);
            contactWs = new ContactWs(objClient);



            user.Age = Int32.Parse(Idade);
            user.Name = Nome;
            user.DddPhoneNumber =NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
            var result = await contactWs.RegisterContact(user);
            if (result != null)
            {
                Erro = "Cadastro efetuado";
            }
            Erro = "Erro no cadastro";
            IsBusy = false;
        }
    }
}
