using AppFVCShared.Validators;
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

        public Command NavegarTerms { get; set; }

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
            if(result == true)
            {
                var val = new NameValidator<string>();
                result = val.Check(_nome);
                ErroNome = val.ValidationMessage;
            }
            else
            {
                ErroNome = "Por favor, informe seu nome.";
            }  
            if(result == false)
            {
                IVNome = true;
            }
            else
            {
                IVNome = false;
            }
            return result;
        }

        public RegisterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            NavegarRegisterInfo = new Command(async () => await NavegarRegisterInfoCommand());
            NavegarTerms = new Command(async () => await NavegarTermsCommand());

            Erro = "";
            ErroNome = "";
            ErroNumero = "";
            ErroIdade = "";
            IVNome = false;
            IVNumero = false;
            IVIdade = false;
            IVErro = false;
        }

        private async Task NavegarTermsCommand()
        {
            await _navigationService.NavigateAsync("TermsPage");
        }

        private async Task NavegarRegisterInfoCommand()
        {
            await _navigationService.NavigateAsync("RegisterInfoPage");
        }

        private  async Task NavegarNextCommand()
        {
            ValidadorNome();
            ValidadorTelefone();
            if (Idade == null || Idade == "")
            {
                IVIdade = true;
                ErroIdade = "Por favor, informe sua idade.";
            }
            else
            {
                IVIdade = false;
                ErroIdade = "";
            }
            if (ValidadorNome() == true && ValidadorTelefone() == true && Idade != null && Idade != "")
            {
                //NovoUsuario.Nome = Nome;
                if (_checkTermo == false)
                {
                    IVErro = true;
                    Erro = "Você precisa aceitar os termos de uso para prosseguir.";

                }
                else
                {
                    var p = new NavigationParameters();
                    p.Add("PhoneNumber", NumeroTelefone);
                    await _navigationService.NavigateAsync("/SmsPage", p);
                    Erro = "";
                }
                

                ErroNome = "";
                ErroNumero = "";
                ErroIdade = "";
            }
            else
            {
                ValidadorNome();
                ValidadorTelefone();
            }
            
        }
    }
}
