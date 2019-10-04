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
        private string _WindowTitle = "��������� �� �������� �����";
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

        //public ObservableCollection<Recipient> Recipients { get; } = new ObservableCollection<Recipient>(); - � ����� ���� �� ����� ����������� ������, ������� ��. ����� ���� � ��-�� ��������������
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

            //var recipients = Recipients; - �������� ��-�� ��������� ��������������� ���� � ��-��
            var recipients = new ObservableCollection<Recipient>();
            //recipients.Clear(); - �.�. ��������� ���� � ��-�� ������������.
            foreach (var recipient in _RecipientsProvider.GetAll()) { recipients.Add(recipient); }
            Recipients = null;//�������� � ����������� ������, ������ ����� �� ���������
            Recipients = recipients;
        }

        public ICommand SaveChangesCommand { get; }
        private bool CanSaveChangesCommandExecute() => true;
        private void OnSaveChangesCommandExecuted() { _RecipientsProvider.SaveChanges(); }
    }
}