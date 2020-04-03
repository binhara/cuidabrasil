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
    public class PreConditionsRiskGroupPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public Command NavegarNext { get; set; }
        public Command SimCommand { get; set; }
        public Command NaoCommand { get; set; }

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
        public PreConditionsRiskGroupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsBusy = false;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            SimCommand = new Command(() => SimCommandExecute());
            NaoCommand = new Command(() => NaoCommandExecute());

            SimColor = "#BDBDBD";
            NaoColor = "#BDBDBD";
        }

        private void NaoCommandExecute()
        {
            NaoColor = "#EE8080";
            SimColor = "#BDBDBD";
        }

        private void SimCommandExecute()
        {
            SimColor = "#6FCF97";
            NaoColor = "#BDBDBD";
        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            await _navigationService.NavigateAsync("/StatusHealthyPage");
        }
    }
}
