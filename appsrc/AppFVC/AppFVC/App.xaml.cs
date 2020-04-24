//
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using Prism;
using Prism.Ioc;
using AppFVC.ViewModels;
using AppFVC.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFVCShared.Services;
using AppFVCShared.Model;
using System.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppFVC
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
          
            var _storeService = new StoreService();
            var datauser = _storeService.FindAll<User>();
            if (!datauser.Any())
            {
                await NavigationService.NavigateAsync("WelcomePage");
            }
            else
            {
              

                await NavigationService.NavigateAsync("/StatusHealthyPage");
            }

        
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<SmsPage, SmsPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<PreConditionsPage, PreConditionsPageViewModel>();
            containerRegistry.RegisterForNavigation<GeoLocationPage, GeoLocationViewModel>();
            containerRegistry.RegisterForNavigation<RegisterInfoPage, RegisterInfoPageViewModel>();
            containerRegistry.RegisterForNavigation<StatusIsolationPage, StatusIsolationPageViewModel>();
            containerRegistry.RegisterForNavigation<StatusQuarantinePage, StatusQuarantinePageViewModel>();
            containerRegistry.RegisterForNavigation<StatusHealthyPage, StatusHealthyPageViewModel>();
            containerRegistry.RegisterForNavigation<TermsPage, TermsPageViewModel>();
            containerRegistry.RegisterForNavigation<CoronaMaps, CoronaMapsViewModel>();
            containerRegistry.RegisterForNavigation<AddressPage, AddressPageViewModel>();
            containerRegistry.RegisterForNavigation<PreConditionsRiskGroupPage, PreConditionsRiskGroupPageViewModel>();
            containerRegistry.RegisterForNavigation<StatusImunePage, StatusImunePageViewModel>();
            containerRegistry.RegisterForNavigation<MedicalGuidanceTermsPage, MedicalGuidanceTermsPageViewModel>();
            containerRegistry.RegisterForNavigation<StatusWebViewPage, StatusWebViewPageViewModel>();
            containerRegistry.RegisterForNavigation<RiskGroupPage, RiskGroupPageViewModel>();



            containerRegistry.RegisterSingleton<IStoreService, StoreService>();

        }
    }
}
