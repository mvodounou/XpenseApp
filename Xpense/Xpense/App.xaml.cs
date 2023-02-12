using System;
using MonkeyCache.FileStore;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xpense
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, ViewModel.MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Views.ExpenseView, ViewModel.ExpenseViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(nameof(MainPage));

            Barrel.ApplicationId = "Xpense";
        }
    }
}

