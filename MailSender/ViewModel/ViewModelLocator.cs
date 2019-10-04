using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation; - стандартная ошибка - удаляем, вместо него - устанавливаем последующий!
using CommonServiceLocator;
using MailSenderLib.Services;
using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Services.Interfaces;
using System;

namespace MailSender.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var services = SimpleIoc.Default;
            ServiceLocator.SetLocatorProvider(() => services);
            services.Register<MainWindowViewModel>();//далее регистристрируем все, что нам может пригодится в качестве сервисов


            //services
            //    .TryRegister<IRecipientsDataProvider, LinkToSQLRecipientsDataProvider>()
            //    .TryRegister(() => new MailSenderDBDataContext());

            services
                .TryRegister<IRecipientsDataProvider, InMemoryRecipientsDataProvider>()
                .TryRegister<ISendersDataProvider, InMemorySendersDataProvider>()
                .TryRegister<IServersDataProvider, InMemoryServersDataProvider>()  ;
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

    public static class SimpleIocExtentions
    {
        public static SimpleIoc TryRegister <TInterface, TService> (this SimpleIoc serivces)
            where TInterface: class
            where TService: class, TInterface
        {
            if (serivces.IsRegistered<TInterface>()) return serivces;
            serivces.Register<TInterface, TService>();
            return serivces;
        }

        public static SimpleIoc TryRegister<TService>(this SimpleIoc serivces)
            where TService : class
        {
            if (serivces.IsRegistered<TService>()) return serivces;
            serivces.Register<TService>();
            return serivces;
        }

        public static SimpleIoc TryRegister<TService>(this SimpleIoc serivces, Func<TService> Factory)
            where TService : class
        {
            if (serivces.IsRegistered<TService>()) return serivces;
            serivces.Register(Factory);
            return serivces;
        }
    }
}