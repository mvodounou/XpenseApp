using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using static System.Net.Mime.MediaTypeNames;
using Xamarin.Essentials;
using System.IO;
using Prism.Navigation;

namespace Xpense.ViewModel
{
    public class ExpenseViewModel : AppViewModel
    {
        private string _expenseSummary;
        public string ExpenseSummary { get => _expenseSummary; set => SetProperty(ref _expenseSummary, value); }

        private string _highestSummary;
        public string HighestSummary { get => _highestSummary; set => SetProperty(ref _highestSummary, value); }

        private int _fuel;
        public int Fuel { get => _fuel; set => SetProperty(ref _fuel, value, nameof(Fuel), () => GetHighestSpend()); }

        private int _parking;
        public int Parking { get => _parking; set => SetProperty(ref _parking, value, nameof(Parking), () => GetHighestSpend()); }

        private int _food;
        public int Food { get => _food; set => SetProperty(ref _food, value, nameof(Food), () => GetHighestSpend()); }

        private ICommand _addAttachmentCommand;
        public ICommand AddAttachmentCommand { get => _addAttachmentCommand; set => SetProperty(ref _addAttachmentCommand, value); }

        private ICommand _dettachCommand;
        public ICommand DettachCommand { get => _dettachCommand; set => SetProperty(ref _dettachCommand, value); }


        private Xamarin.Forms.ImageSource _attachmentSource;
        public Xamarin.Forms.ImageSource AttachmentSource { get => _attachmentSource; set => SetProperty(ref _attachmentSource, value); }

        private bool _isAttachmentVisible;
        public bool IsAttachmentVisible { get => _isAttachmentVisible; set => SetProperty(ref _isAttachmentVisible, value); }

        private bool _isDettachBtnVisible;
        public bool IsDettachBtnVisible { get => _isDettachBtnVisible; set => SetProperty(ref _isDettachBtnVisible, value); }

        INavigationService _navigationService;
        public ExpenseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddAttachmentCommand = new Command(AttemptAttach);
            DettachCommand = new Command(AttemptDetachment);

            MainTemplateTitle = "Expense";
            MainTemplateMenuText = "Back";
            MainTemplateMenuCommand = new Command(AttemptBackNavigation);
        }

        private void AttemptBackNavigation(object obj)
        {
            _navigationService.GoBackAsync();
        }

        private void AttemptDetachment(object obj)
        {
            IsAttachmentVisible = false;
            AttachmentSource = null;
            IsDettachBtnVisible = false;
        }

        private async void AttemptAttach(object obj)
        {
            await PickAttachment();
        }

        private void GetHighestSpend()
        {
            ExpenseSummary = $"Total expense claim is £{Fuel + Food + Parking}.";

            var HighestSpend = Math.Max(Math.Max(Fuel, Parking), Food);

            if (HighestSpend == 0)
                HighestSummary = string.Empty;
            if (HighestSpend == Food)
                HighestSummary = "The highest category was food";
            else if (HighestSpend == Parking)
                HighestSummary = "The highest category was Parking";
            else if (HighestSpend == Fuel)
                HighestSummary = "The highest category was fuel";
            else
                HighestSummary = string.Empty;
        }

        async Task PickAttachment()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions { PickerTitle = "Pick Attachment", FileTypes = FilePickerFileType.Images });
                if (result != null)
                {
                    //Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        AttachmentSource = Xamarin.Forms.ImageSource.FromStream(() => stream);
                    }
                }

                IsAttachmentVisible = true;
                IsDettachBtnVisible = true;
            }
            catch (Exception)
            {
                IsDettachBtnVisible = false;
            }
        }
    }
}

