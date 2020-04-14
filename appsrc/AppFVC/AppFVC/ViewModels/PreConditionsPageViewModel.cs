using AppFVCShared.Model;
using AppFVCShared.Services;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppFVC.ViewModels
{
    public class PreConditionsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        readonly IStoreService _storeService;

        private bool AlreadyInitialized;
        private bool IsLabel;

        private ObservableCollection<Comorbidity> _ComorbidityItems;
        public ObservableCollection<Comorbidity> ComorbidityItems
        {
            get { return _ComorbidityItems; }
            set { SetProperty(ref _ComorbidityItems, value); }
        }

        public Command NavegarNext { get; set; }
        public Command HipertensaoCommand { get; set; }
        public Command RespiratoriaCommand { get; set; }
        public Command DiabetesCommand { get; set; }
        public Command CardioCommand { get; set; }
        public Command RenalCommand { get; set; }
        public Command ImunodefCommand { get; set; }


        #region Propriedades

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
                { SetProperty(ref _lbRenal, value);}
                if (AlreadyInitialized)
                    if(!IsLabel)
                        RenalCommandExecute("checkbox");

            }
        }

        private bool _lbCardio;
        public bool lbCardio
        {
            get { return _lbCardio; }
            set
            {
                { SetProperty(ref _lbCardio, value); }
                if (AlreadyInitialized)
                    if (!IsLabel)
                        CardioCommandExecute("checkbox");
            }
        }


        private bool _lbImunodef;
        public bool LbImunodef
        {
            get { return _lbImunodef; }
            set
            {
                { SetProperty(ref _lbImunodef, value); }
                if (AlreadyInitialized)
                    if (!IsLabel)
                        ImunodefCommandExecute("checkbox");
            }
        }


        private bool _lbDiabetes;
        public bool lbDiabetes
        {
            get { return _lbDiabetes; }
            set
            {
                { SetProperty(ref _lbDiabetes, value); }
                if (AlreadyInitialized)
                    if (!IsLabel)
                        DiabetesCommandExecute("checkbox");
            }
        }

        private bool _lbRespiratoria;
        public bool lbRespiratoria
        {
            get { return _lbRespiratoria; }
            set
            {
                { SetProperty(ref _lbRespiratoria, value); }
                if (AlreadyInitialized)
                    if (!IsLabel)
                        RespiratoriaCommandExecute("checkbox");
            }
        }


        private bool _lbHipertensao;
        public bool lbHipertensao
        {
            get { return _lbHipertensao; }
            set
            {
                { SetProperty(ref _lbHipertensao, value); }
                if (AlreadyInitialized)
                    if (!IsLabel)
                        HipertensaoCommandExecute("checkbox");
            }
        }

        #endregion


        public PreConditionsPageViewModel(INavigationService navigationService, IStoreService storeService) : base(navigationService)
        {
            
            _storeService = storeService;
            ComorbidityItems = new ObservableCollection<Comorbidity>();
            ComorbidityItems = Comorbidity.GetList();
            IsBusy = false;
            _navigationService = navigationService;
            NavegarNext = new Command(async () => await NavegarNextCommand());

            HipertensaoCommand = new Command(() => HipertensaoCommandExecute("label"));
            RespiratoriaCommand = new Command(() => RespiratoriaCommandExecute("label"));
            DiabetesCommand = new Command(() => DiabetesCommandExecute("label"));
            ImunodefCommand = new Command(() => ImunodefCommandExecute("label"));
            CardioCommand = new Command(() => CardioCommandExecute("label"));
            RenalCommand = new Command(() => RenalCommandExecute("label"));


            lbHipertensao = false;
            lbRespiratoria = false;
            lbDiabetes = false;
            LbImunodef = false;
            lbCardio = false;
            lbRenal = false;
            AlreadyInitialized = true;
        }

        public void SaveComorbityItem(string name, bool isPositive)
        {
            var item = ComorbidityItems.FirstOrDefault(i => i.Name == name);
            if (item != null)
            {
                item.IsPositive = isPositive;
            }
        }

        private void RenalCommandExecute(string propriedade)
        {
            if (propriedade == "label")
            {
                IsLabel = true;
                if (lbRenal == false)
                {
                    lbRenal = true;                    
                }
                else
                {
                    lbRenal = false;
                }
                SaveComorbityItem("Insuficiência Renal", lbRenal);
            }
            else if(propriedade == "checkbox")
            {
                SaveComorbityItem("Insuficiência Renal", lbRenal);
            }
            IsLabel = false;
        }
        private void CardioCommandExecute(string propriedade)
        {
            if (lbCardio == false)
            {
                if (propriedade == "label")
                {
                    IsLabel = true;
                    lbCardio = true;
                }
                SaveComorbityItem("Cardíaco", lbCardio);
            }
            else
            {
                if (propriedade == "label")
                {
                    IsLabel = true;
                    lbCardio = false;
                }
                SaveComorbityItem("Cardíaco", lbCardio);
            }
            IsLabel = false;
        }

        private void ImunodefCommandExecute(string propriedade)
        {
            if (propriedade == "label")
            {
                IsLabel = true;
                if (LbImunodef == false)
                {
                    LbImunodef = true;
                }
                else
                {
                    LbImunodef = false;
                }
                SaveComorbityItem("Fumante", LbImunodef);
            }
            else if (propriedade == "checkbox")
            {
                SaveComorbityItem("Fumante", LbImunodef);
            }
            IsLabel = false;
        }

        private void DiabetesCommandExecute(string propriedade)
        {
            if (propriedade == "label")
            {
                IsLabel = true;
                if (lbDiabetes == false)
                {
                    lbDiabetes = true;
                }
                else
                {
                    lbDiabetes = false;
                }
                SaveComorbityItem("Diabetes", lbDiabetes);
            }
            else if (propriedade == "checkbox")
            {
                SaveComorbityItem("Diabetes", lbDiabetes);
            }
            IsLabel = false;

        }

        private void RespiratoriaCommandExecute(string propriedade)
        {
            if (propriedade == "label")
            {
                IsLabel = true;
                if (lbRespiratoria == false)
                {
                    lbRespiratoria = true;
                }
                else
                {
                    lbRespiratoria = false;
                }
                SaveComorbityItem("Asma", lbRespiratoria);
            }
            else if (propriedade == "checkbox")
            {
                SaveComorbityItem("Asma", lbRespiratoria);
            }
            IsLabel = false;
        }

        private void HipertensaoCommandExecute(string propriedade)
        {
            if (propriedade == "label")
            {
                IsLabel = true;
                if (lbHipertensao == false)
                {
                    lbHipertensao = true;
                }
                else
                {
                    lbHipertensao = false;
                }
                SaveComorbityItem("Hipertensão", lbHipertensao);
            }
            else if (propriedade == "checkbox")
            {
                SaveComorbityItem("Hipertensão", lbHipertensao);
            }
            IsLabel = false;
        }


        private async Task NavegarNextCommand()
        {
            IsBusy = true;
            
            SaveUser();
            await _navigationService.NavigateAsync("/PreConditionsRiskGroupPage");
        }

        private void SaveUser()
        {
            var users = _storeService.FindAll<User>();
            if (users != null)
            {
                _storeService.RemoveAll<User>();
            }
            var user = users.ToList()[0];
            user.Comorbidities = ComorbidityItems;
            _storeService.Store<User>(user);

            users = _storeService.FindAll<User>();
        }
    }
}
