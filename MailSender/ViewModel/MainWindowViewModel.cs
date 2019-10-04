using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSenderLib.Entities;
//using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Services;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _WindowTitle = "Программа по рассылке почты";
        public string WindowTitle
        {
            get => _WindowTitle;
            set => Set(ref _WindowTitle, value);
        }

        private Recipient _SelectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient; set => Set(ref _SelectedRecipient, value);
        }

        //public ObservableCollection<Recipient> Recipients { get; } = new ObservableCollection<Recipient>(); - в таком виде не сразу обновляются данные, поэтому см. далее поле и св-во соответственно
        private ObservableCollection<Recipient> _Recipients = new ObservableCollection<Recipient>();
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }




        private IRecipientsDataProvider _RecipientsProvider;
        private ISendersDataProvider _SendersProvider;
        private IServersDataProvider _ServersProvider;

        public MainWindowViewModel(IRecipientsDataProvider RecipientsDataProvider,
            ISendersDataProvider SendersDataProvider, 
            IServersDataProvider ServersDataProvider)
        {
            _RecipientsProvider = RecipientsDataProvider;
            RefrashDataCommand = new RelayCommand(OnRefrashDataCommandExecuted,CanRefrashDataCommandExecute);
            //RefreshData();
            _SendersProvider = SendersDataProvider;
            _ServersProvider = ServersDataProvider;
        }

        public ICommand RefrashDataCommand { get; }
        private bool CanRefrashDataCommandExecute() =>true;
        private void OnRefrashDataCommandExecuted() { RefreshData(); }
        private void RefreshData()
        {

            //var recipients = Recipients; - изменено из-за появления соответствующих поля и св-ва
            var recipients = new ObservableCollection<Recipient>();
            //recipients.Clear(); - т.к. появились поле и св-ва обзервэблкол.
            foreach (var recipient in _RecipientsProvider.GetAll()) { recipients.Add(recipient); }
            Recipients = null;//боролись с обновлением данных, скорее всего не требуется
            Recipients = recipients;
        }

        public ICommand SaveChangesCommand { get; }
        private bool CanSaveChangesCommandExecute() => true;
        private void OnSaveChangesCommandExecuted() { _RecipientsProvider.SaveChanges(); }
    }
}