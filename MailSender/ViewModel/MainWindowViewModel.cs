using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _WindowTitle = "��������� �� �������� �����";
        private RecipientsDataProvider _RecipientsProvider;
        public string WindowTitle
        {
            get => _WindowTitle;
            set => Set(ref _WindowTitle, value);
        }

        private Recipient _SelectedRecipientIs;
        public Recipient SelectedRecipientIs
        {
            get => _SelectedRecipientIs; set => Set(ref _SelectedRecipientIs, value);
        }

        //public ObservableCollection<Recipient> Recipients { get; } = new ObservableCollection<Recipient>(); - � ����� ���� �� ����� ����������� ������, ������� ��. ����� ���� � ��-�� ��������������
        private ObservableCollection<Recipient> _Recipients = new ObservableCollection<Recipient>();
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }

        public ICommand RefrashDataCommand { get; }

        public MainWindowViewModel(RecipientsDataProvider RecipientsProvaider)
        {
            _RecipientsProvider = RecipientsProvaider;
            RefrashDataCommand = new RelayCommand(OnRefrashDataCommandExecuted,CanRefrashDataCommandExecute);
            //RefreshData();
        }

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
    }
}