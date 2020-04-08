using AppFVCShared.Model;
using Prism.Mvvm;
using Prism.Navigation;

namespace AppFVC.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        public static bool IsRunningSms;

        private static User _appUser;

        public User AppUser
        {
            get => _appUser;
            set => _appUser = value;
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            if (AppUser == null)
                AppUser = new User();
        }

    

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
