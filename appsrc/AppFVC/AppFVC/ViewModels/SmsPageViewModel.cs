using AppFVCShared.Sms;
using AppFVCShared.WebService;
using Prism.Navigation;
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
            NumeroTelefone = AppUser.DddPhoneNumber;
            LabelTelefone = "O código foi enviado para o número " + NumeroTelefone;

//#if DEBUG
//            Codigo = "123456";
//#endif
            SendSMSAsync();
        }

        private async Task SendSMSAsync()
        {
            var gerador = new GeneratorOtp();
            gerador.GenerateOtp();
            CodigoSms = gerador.SmsCode;

            ClientSms cSms = new ClientSms();
            var phoneNumber = "55" + (NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", ""));
            var result = cSms.SendSMSAsync(phoneNumber, CodigoSms);

            VisibleErro = true;
            if (result != null)
            {
                //Sucesso
                Erro = "SMS enviado com sucesso!";
            }
            else
            {
                // Erro no envio 
                Erro = "Problema no envio do SMS!";
            }


        }

        private async Task NavegarNextCommand()
        {
            if (_codigo == null || _codigo == "")
            {
                VisibleErro = true;
                Erro = "Código inválido! Tente novamente.";
            }
            else if (_codigo.Length < 6)
            {
                VisibleErro = true;
                Erro = "Código inválido! Tente novamente.";
            }
            else if (_codigo.Contains(","))
            {
                VisibleErro = true;
                Erro = "Código inválido! Tente novamente.";
            }
            else if(Codigo == CodigoSms)
            {
                VisibleErro = false;
                await _navigationService.NavigateAsync("/PreConditionsPage");
            }
            else
            {
                VisibleErro = true;
                Erro = "Código inválido! Tente novamente.";
            }

        }
    }
}
