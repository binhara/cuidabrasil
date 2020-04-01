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
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
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
                ErroNumero = "Por favor, informe seu telefone.";
            }
            if (result == false)
            {
                IVNumero = true;
            }
            else
            {
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
                ErroNome = "Por favor, informe seu nome.";
            }
            if (result == false)
            {
                IVNome = true;
            }
            else
            {
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
            IsBusy = false;

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
                ErroIdade = "Por favor, informe sua idade.";
                IsBusy = false;
            }
            else
            {
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
                    AppUser.DddPhoneNumber = "+55" + NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "")
                                               .Replace("-", "");
                    AppUser.Age = Int32.Parse(Idade);



                    await _navigationService.NavigateAsync("/SmsPage");
                    IsBusy = false;




                    Erro = "";
                }
                IsBusy = false;

                ErroNome = "";
                ErroNumero = "";
                ErroIdade = "";
            }
            else
            {
                ValidadorNome();
                ValidadorTelefone();
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
