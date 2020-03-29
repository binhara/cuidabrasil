using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class SmsPageViewModel : BindableBase
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
        public SmsPageViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
            VisibleErro = false;
            NavegarNext = new Command(async () => await NavegarNextCommand());
#if DEBUG
            Codigo = "123456";
#endif

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
            else
            {
                VisibleErro = false;
                _navigationService.NavigateAsync("PreConditionsPage");
            }
            
        }
    }
}
