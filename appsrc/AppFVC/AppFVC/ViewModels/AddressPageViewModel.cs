//
// Journal.cs: Assignments.
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
using AppFVCShared.Validators;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class AddressPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarNext { get; set; }

        #region Binding IsVisible

        private bool _iVCep;
        public bool IVCep
        {
            get { return _iVCep; }
            set
            {
                { SetProperty(ref _iVCep, value); }
            }
        }

        private bool _iVEndereco;
        public bool IVEndereco
        {
            get { return _iVEndereco; }
            set
            {
                { SetProperty(ref _iVEndereco, value); }
            }
        }

        private bool _iVNumero;
        public bool IVNumero
        {
            get { return _iVNumero; }
            set
            {
                { SetProperty(ref _iVNumero, value); }
            }
        }

        private bool _iVBairro;
        public bool IVBairro
        {
            get { return _iVBairro; }
            set
            {
                { SetProperty(ref _iVBairro, value); }
            }
        }

        #endregion

        #region Binding Erro
        private string _erroCep;
        public string ErroCep
        {
            get { return _erroCep; }
            set
            {
                SetProperty(ref _erroCep, value);
                RaisePropertyChanged("ErroCep");

            }
        }

        private string _erroEndereco;
        public string ErroEndereco
        {
            get { return _erroEndereco; }
            set
            {
                SetProperty(ref _erroEndereco, value);
                RaisePropertyChanged("ErroEndereco");

            }
        }

        private string _erroNumero;
        public string ErroNumero
        {
            get { return _erroNumero; }
            set
            {
                SetProperty(ref _erroNumero, value);
                RaisePropertyChanged("ErroNumero");

            }
        }

        private string _erroBairro;
        public string ErroBairro
        {
            get { return _erroBairro; }
            set
            {
                SetProperty(ref _erroBairro, value);
                RaisePropertyChanged("ErroBairro");

            }
        }

        #endregion

        #region Binding TxtColor

        private string _txtColorCep;
        public string TxtColorCep
        {
            get { return _txtColorCep; }
            set
            {
                if (_txtColorCep != value)
                {
                    _txtColorCep = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _txtColorEndereco;
        public string TxtColorEndereco
        {
            get { return _txtColorEndereco; }
            set
            {
                if (_txtColorEndereco != value)
                {
                    _txtColorEndereco = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _txtColorNumero;
        public string TxtColorNumero
        {
            get { return _txtColorNumero; }
            set
            {
                if (_txtColorNumero != value)
                {
                    _txtColorNumero = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _txtColorBairro;
        public string TxtColorBairro
        {
            get { return _txtColorBairro; }
            set
            {
                if (_txtColorBairro != value)
                {
                    _txtColorBairro = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Binding Border
        private string _borderCep;
        public string borderCep
        {
            get { return _borderCep; }
            set
            {
                if (_borderCep != value)
                {
                    _borderCep = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Binding Campos

        private string _cep;
        public string Cep
        {
            get
            {
                return _cep;
            }
            set
            {
                SetProperty(ref _cep, value);
                RaisePropertyChanged("Cep");
                if (TxtColorCep != "#222222")
                {
                    TxtColorCep = "#222222";
                }
            }
        }

        private string _endereco;
        public string Endereco
        {
            get
            {
                return _endereco;
            }
            set
            {
                SetProperty(ref _endereco, value);
                RaisePropertyChanged("Endereco");
                if (TxtColorEndereco != "#222222")
                {
                    TxtColorEndereco = "#222222";
                }
            }
        }

        private string _numero;
        public string Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                SetProperty(ref _numero, value);
                RaisePropertyChanged("Numero");
                if (TxtColorNumero != "#222222")
                {
                    TxtColorNumero = "#222222";
                }
            }
        }

        private string _complemento;
        public string Complemento
        {
            get
            {
                return _complemento;
            }
            set
            {
                SetProperty(ref _complemento, value);
                RaisePropertyChanged("Complemento");
            }
        }

        private string _bairro;
        public string Bairro
        {
            get
            {
                return _bairro;
            }
            set
            {
                SetProperty(ref _bairro, value);
                RaisePropertyChanged("Bairro");
                if (TxtColorBairro != "#222222")
                {
                    TxtColorBairro = "#222222";
                }
            }
        }

        #endregion

        public bool ValidadorCep()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_cep);
            if (result == true)
            {
                if (_cep.Length < 10)
                {
                    result = false;
                    ErroCep = "CEP inválido!";
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                TxtColorCep = "#EB5757";
                ErroCep = "Por favor, informe seu CEP.";
            }
            if (result == false)
            {
                TxtColorCep = "#EB5757";
                IVCep = true;
            }
            else
            {
                TxtColorCep = "#222222";
                IVCep = false;
            }
            return result;
        }

        public bool ValidadorAddress()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_endereco);
            if (result == true)
            {
                var val = new AddressValidator<string>();
                result = val.Check(_endereco);
                ErroEndereco = "Endereço inválido!";
            }
            else
            {
                TxtColorEndereco = "#EB5757";
                ErroEndereco = "Por favor, informe seu endereço.";
            }
            if (result == false)
            {
                TxtColorEndereco = "#EB5757";
                IVEndereco = true;
            }
            else
            {
                TxtColorEndereco = "#222222";
                IVEndereco = false;
            }
            return result;
        }

        public bool ValidadorNumero()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_numero);
            if (result == false)
            {
                TxtColorNumero = "#EB5757";
                ErroNumero = "Por favor, informe seu número.";
                IVNumero = true;
            }
            else
            {
                TxtColorNumero = "#222222";
                IVNumero = false;
            }
            return result;
        }

        public bool ValidadorBairro()
        {
            var validacao = new IsNotNullOrEmptyRule<string>();
            var result = validacao.Check(_bairro);
            if (result == true)
            {
                var val = new AddressValidator<string>();
                result = val.Check(_bairro);
                ErroBairro = "Bairro inválido!";
            }
            else
            {
                TxtColorBairro = "#EB5757";
                ErroBairro = "Por favor, informe seu bairro.";
            }
            if (result == false)
            {
                TxtColorBairro = "#EB5757";
                IVBairro = true;
            }
            else
            {
                TxtColorBairro = "#222222";
                IVBairro = false;
            }
            return result;
        }

        public AddressPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());

            //Erro = "";
            #region Binding Componentes
            TxtColorCep = "#222222";
            TxtColorEndereco = "#222222";
            TxtColorNumero = "#222222";
            TxtColorBairro = "#222222";
            ErroCep = "";
            ErroEndereco = "";
            ErroNumero = "";
            ErroBairro = "";
            IVCep = false;
            IVEndereco = false;
            IVNumero = false;
            IVBairro = false;
            borderCep = "0";
            #endregion
#if DEBUG
            Cep = "88063300";
            Endereco = "Avenida Campeche";
            Numero = "203";
            Bairro = "Lagoa Pequena";
#endif
        }

        private async Task NavegarNextCommand()
        {
            ValidadorCep();
            ValidadorAddress();
            ValidadorNumero();
            ValidadorBairro();

            if (ValidadorAddress() && ValidadorCep() && ValidadorNumero() && ValidadorBairro())
            {
                await _navigationService.NavigateAsync("/PreConditionsPage");
            }
            else
            {
                ValidadorCep();
                ValidadorAddress();
                ValidadorNumero();
                ValidadorBairro();
            }
        }
    }
}
