using AppFVCShared.Model;
using AppFVCShared.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace AppFVC.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        public static bool IsRunningSms;

        protected readonly IStoreService _storeService;
        protected INavigationService NavigationService { get; private set; }

        private static ICacheService _cacheService;
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

        public ViewModelBase(INavigationService navigationService, IStoreService storeService = null)
        {
            if (_cacheService == null)
                _cacheService = new CacheService(DependencyService.Get<IStoreService>());

            _storeService = storeService;

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
