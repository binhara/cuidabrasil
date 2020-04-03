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

        public Command HipertensaoCommand { get; set; }
        public Command RespiratoriaCommand { get; set; }
        public Command DiabetesCommand { get; set; }
        public Command CardioCommand { get; set; }
        public Command RenalCommand { get; set; }
        public Command ImunodefCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                { SetProperty(ref _isBusy, value); }
            }
        }

        private bool _lbRenal;
        public bool lbRenal
        {
            get { return _lbRenal; }
            set
            {
                { SetProperty(ref _lbRenal, value); }
            }
        }

        private bool _lbCardio;
        public bool lbCardio
        {
            get { return _lbCardio; }
            set
            {
                { SetProperty(ref _lbCardio, value); }
            }
        }


        private bool _lbImunodef;
        public bool LbImunodef
        {
            get { return _lbImunodef; }
            set
            {
                { SetProperty(ref _lbImunodef, value); }
            }
        }


        private bool _lbDiabetes;
        public bool lbDiabetes
        {
            get { return _lbDiabetes; }
            set
            {
                { SetProperty(ref _lbDiabetes, value); }
            }
        }

        private bool _lbRespiratoria;
        public bool lbRespiratoria
        {
            get { return _lbRespiratoria; }
            set
            {
                { SetProperty(ref _lbRespiratoria, value); }
            }
        }


        private bool _lbHipertensao;
        public bool lbHipertensao
        {
            get { return _lbHipertensao; }
            set
            {
                { SetProperty(ref _lbHipertensao, value); }
            }
        }

       
        public PreConditionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            IsBusy = false;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());

            HipertensaoCommand = new Command(async () => await HipertensaoCommandExecute());
            RespiratoriaCommand = new Command(async () => await RespiratoriaCommandExecute());
            DiabetesCommand = new Command(async () => await DiabetesCommandExecute());
            ImunodefCommand = new Command(async () => await ImunodefCommandExecute());
            CardioCommand = new Command(async () => await CardioCommandExecute());
            RenalCommand = new Command(async () => await RenalCommandExecute());


            lbHipertensao = false;
            lbRespiratoria = false;
            lbDiabetes = false;
            LbImunodef = false;
            lbCardio = false;
            lbRenal = false;
        }

        private async Task RenalCommandExecute()
        {
            if (lbRenal == false)
            {
                lbRenal = true;
            }
            else
            {
                lbRenal = false;
            }
         
        }

        private async Task CardioCommandExecute()
        {
            if (lbCardio == false)
            {
                lbCardio = true;
            }
            else
            {
                lbCardio = false;
            }
          
        }

        private async Task ImunodefCommandExecute()
        {
            if (LbImunodef == false)
            {
                LbImunodef = true;
            }
            else
            {
                LbImunodef = false;
            }

        }

        private async  Task DiabetesCommandExecute()
        {
             if (lbDiabetes == false)
            {
                lbDiabetes = true;
            }
            else
            {
                lbDiabetes = false;
            }
           
        }

        private async Task RespiratoriaCommandExecute()
        {
            if (lbRespiratoria == false)
            {
                lbRespiratoria = true;
            }
            else
            {
                lbRespiratoria = false;
            }
         
        }

        private async Task HipertensaoCommandExecute()
        {
            if (lbHipertensao == false)
            {
                lbHipertensao = true;
            }
            else
            {
                lbHipertensao = false;
            }
           
        }


        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            _navigationService.NavigateAsync("/PreConditionsRiskGroupPage");
        }
    }
}
