using GalaSoft.MvvmLight;
using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Services;
using System;
using System.Collections.ObjectModel;
namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _WindowTitle = "Программа по рассылке почты";
        private RecipientsDataProvider _RecipientsProvider;
        public string WindowTitle
        {
            get => _WindowTitle;
            set => Set(ref _WindowTitle, value);
        }

        public ObservableCollection<Recipient> Recipients { get; } = new ObservableCollection<Recipient>();

        public MainWindowViewModel(RecipientsDataProvider RecipientsProvaider)
        {
            _RecipientsProvider = RecipientsProvaider;
            RefreshData();
        }

        private void RefreshData()
        {
            var recipients = Recipients;
            recipients.Clear();
            foreach (var recipient in _RecipientsProvider.GetAll()) { recipients.Add(recipient); }
        }
    }
}