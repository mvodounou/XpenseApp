using System;
using System.Windows.Input;
using MonkeyCache.FileStore;
using MvvmHelpers.Commands;
using Prism.Navigation;
using Xpense.Views;

namespace Xpense.ViewModel
{
    public class MainPageViewModel : AppViewModel, INavigatedAware
    {
        private bool _isBtnEnabled;
        public bool IsBtnEnabled { get => _isBtnEnabled; set => SetProperty(ref _isBtnEnabled, value); }

        private bool _isLogInAttempt;
        public bool IsLogInAttemptSuccessfull { get => _isLogInAttempt; set => SetProperty(ref _isLogInAttempt, value); }

        private string _userName;
        public string Username { get => _userName; set => SetProperty(ref _userName, value, nameof(Username), () => EntryDataChaged(_userName)); }


        private string _passWord;
        public string Password { get => _passWord; set => SetProperty(ref _passWord, value, nameof(_passWord), () => EntryDataChaged(_passWord)); }

        private void EntryDataChaged(string value)
        {
            IsBtnEnabled = !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username);
        }

        public ICommand LogInCommand { get; set; }

        INavigationService _navigationService;
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LogInCommand = new Command(AttemptLogIn);
            MainTemplateMenuCommand = new Command(AttemptMenuOptions);
        }

        private void AttemptMenuOptions(object obj)
        {
            //throw new NotImplementedException();
        }

        private void AttemptLogIn()
        {
            IsLogInAttemptSuccessfull = !string.Equals(Password, "password", StringComparison.CurrentCultureIgnoreCase);
            if (!IsLogInAttemptSuccessfull)
            {
                Barrel.Current.Add(key: "lastLogIn", Username, TimeSpan.FromDays(365));
                _navigationService.NavigateAsync(nameof(ExpenseView));
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            MainTemplateTitle = "Login";
            MainTemplateMenuText = "Menu";

            if (Barrel.Current.Exists(key: "lastLogIn"))
            {
                Username = Barrel.Current.Get<string>(key: "lastLogIn");
            }
        }

    }
}

