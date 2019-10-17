using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation; - стандартная ошибка - удаляем, вместо него - устанавливаем последующий!
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
            services.Register<MainWindowViewModel>();//далее регистристрируем все, что нам может пригодится в качестве сервисов


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
        }  //первая встроенная вьюмодель. Доступ к ней через это св-во Main. Сюда далее по аналогии можно добавлять и свои вьюмодели и так же получать к ним доступ черз св-во
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}