using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppFVC.ViewModels
{
    public class PreConditionsPageViewModel : ViewModelBase
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
       
        public PreConditionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsBusy = false;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());
            SimCommand = new Command(async () => await SimCommandExecute());
            NaoCommand = new Command(async () => await NaoCommandExecute());
            SimColor = "#808080";
            NaoColor = "#808080";
        }

        private async Task NaoCommandExecute()
            {
            NaoColor = "#FF0000";
            SimColor = "#808080";
        }

        private async Task SimCommandExecute()
        {
            SimColor = "#00FF00";
            NaoColor = "#808080";
        }

        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            _navigationService.NavigateAsync("/StatusHealthyPage");
        }
    }
}
