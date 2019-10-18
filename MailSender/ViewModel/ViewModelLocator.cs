using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation; - ����������� ������ - �������, ������ ���� - ������������� �����������!
using CommonServiceLocator;
using MailSenderLib.Services;
using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Services.Interfaces;
using MailSenderLib.Data.EF;
using MailSenderLib.Services.InMemory;
using MailSenderLib.Services.EF;

namespace MailSender.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var services = SimpleIoc.Default;
            ServiceLocator.SetLocatorProvider(() => services);
            services.Register<MainWindowViewModel>();//����� ���������������� ���, ��� ��� ����� ���������� � �������� ��������


            services
                .TryRegister<IRecipientsDataProvider, LinkToSQLRecipientsDataProvider>()
               // .TryRegister<ISendersDataProvider, InMemorySendersDataProvider>()!!!!!!
               // .TryRegister<IServersDataProvider, InMemoryServersDataProvider>()!!!!!!
                .TryRegister(() => new MailSenderDBDataContext())
                .TryRegister<MemoryDataContext>()
                .TryRegister<DataContextProvider>();
            //.TryRegister(() => new MailSenderDB());

            //services
            //    .TryRegister<IRecipientsDataProvider, InMemoryRecipientsDataProvider>()
            //    .TryRegister<ISendersDataProvider, InMemorySendersDataProvider>()
            //    .TryRegister<IServersDataProvider, InMemoryServersDataProvider>();
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