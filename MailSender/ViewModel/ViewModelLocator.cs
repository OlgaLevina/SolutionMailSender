using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation; - ����������� ������ - �������, ������ ���� - ������������� �����������!
using CommonServiceLocator;
using MailSenderLib.Services;
using MailSenderLib.Data.LinqToSQL;

namespace MailSender.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var services = SimpleIoc.Default;
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            services.Register<MainWindowViewModel>();//����� ���������������� ���, ��� ��� ����� ���������� � �������� ��������
            services.Register<LinkToSQLRecipientsDataProvider>();
            services.Register(()=> new MailSenderDBDataContext());
        }

        public MainWindowViewModel MainWindowModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }  //������ ���������� ���������. ������ � ��� ����� ��� ��-�� Main. ���� ����� �� �������� ����� ��������� � ���� ��������� � ��� �� �������� � ��� ������ ���� ��-��
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}