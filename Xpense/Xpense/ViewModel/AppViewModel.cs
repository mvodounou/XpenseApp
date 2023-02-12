using System;
using System.Windows.Input;
using MvvmHelpers;

namespace Xpense.ViewModel
{
    public class AppViewModel : BaseViewModel
    {
        private string _mainTemplateTitle;
        public string MainTemplateTitle { get => _mainTemplateTitle; set => SetProperty(ref _mainTemplateTitle, value); }

        private string _mainTemplateMenuText;
        public string MainTemplateMenuText { get => _mainTemplateMenuText; set => SetProperty(ref _mainTemplateMenuText, value); }

        private ICommand _mainTemplateMenuCommand;
        public ICommand MainTemplateMenuCommand { get => _mainTemplateMenuCommand; set => SetProperty(ref _mainTemplateMenuCommand, value); }

        public AppViewModel()
        {

        }
    }
}

