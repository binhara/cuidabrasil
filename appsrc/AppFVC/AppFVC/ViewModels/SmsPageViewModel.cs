using AppFVCShared.Model;
using AppFVCShared.Sms;
using AppFVCShared.WebService;
using FCVLibWS;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class SmsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
       
        public Command NavegarNext { get; set; }
        public Command NavegarBack { get; set; }
        public Command ReenviarCod { get; set; }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }

        private string _changeButtonColor;
        public string ChangeButtonColor
        {
            get { return _changeButtonColor; }
            set
            {
                if (_changeButtonColor != value)
                {
                    _changeButtonColor = value;
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
                if (_erro != value)
                {
                    _erro = value;
                    OnPropertyChanged();
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
            }
        }

        private string _labelTelefone;
        public string LabelTelefone
        {
            get
            {
                return _labelTelefone;
            }

            set
            {
                SetProperty(ref _labelTelefone, value);
                RaisePropertyChanged("Telefone");
            }
        }
        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (_codigo != value)
                {
                    _codigo = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _codigoSms;
        public string CodigoSms
        {
            get { return _codigoSms; }
            set
            {
                if (_codigoSms != value)
                {
                    _codigoSms = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _visibleErro;
        public bool VisibleErro
        {
            get { return _visibleErro; }
            set
            {
                if (_visibleErro != value)
                {
                    _visibleErro = value;
                    OnPropertyChanged();
                }
            }
        }
        private static bool Enviado;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SmsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
           
            _navigationService = navigationService;
            VisibleErro = false;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            NavegarBack = new Command(async () => await NavegarBackCommand());
            ReenviarCod = new Command(async () => await SendSMSAsync());
            NumeroTelefone = AppUser.DddPhoneNumber;
            IsBusy = false;
            //AppUser.DddPhoneNumber = NumeroTelefone;
            LabelTelefone = "O código foi enviado para o número " + NumeroTelefone;
            Enviado = false;
//#if DEBUG
//            Codigo = "123456";
//#endif
            SendSMSAsync();
        }

        private async Task NavegarBackCommand()
        {
            IsBusy = true;
            await _navigationService.NavigateAsync("/RegisterPage");
        }

        private async Task SendSMSAsync()
        {
            IsBusy = true;
            var gerador = new GeneratorOtp();
            gerador.GenerateOtp();
            CodigoSms = gerador.SmsCode;

            ClientSms cSms = new ClientSms();
            var phoneNumber = "55" + (NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", ""));
            var result = cSms.SendSMSAsync(phoneNumber, CodigoSms);

            VisibleErro = true;
            if (result != null)
            {
                if (Enviado)
                {
                    Erro = "Novo SMS enviado com sucesso!";
                    IsBusy = false;
                }
                else
                {
                    //Sucesso
                    Erro = "Código enviado com sucesso!";
                    IsBusy = false;
                }
                Enviado = true;
            }
            else
            {
                // Erro no envio 
                Erro = "Problema no envio do SMS!";
                IsBusy = false;

            }


        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            if (_codigo == null || _codigo == "")
            {
                VisibleErro = true;
                Erro = "Código inválido! Tente novamente.";
                IsBusy = false;
            }
            else if (_codigo.Length < 6)
            {
                VisibleErro = true;
                IsBusy = false;
                Erro = "Código inválido! Tente novamente.";
            }
            else if (_codigo.Contains(","))
            {
                VisibleErro = true;
                IsBusy = false;
                Erro = "Código inválido! Tente novamente.";
            }
            else if(Codigo == CodigoSms)
            {
                VisibleErro = false;
                IsBusy = false;
                await _navigationService.NavigateAsync("/AddressPage");
            }
            else
            {
                VisibleErro = true;
                IsBusy = false;
                Erro = "Código inválido! Tente novamente.";
            }

        }
    }
}
