using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation; - стандартная ошибка - удаляем, вместо него - устанавливаем последующий!
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
            services.Register<MainWindowViewModel>();//далее регистристрируем все, что нам может пригодится в качестве сервисов
            services.Register<LinkToSQLRecipientsDataProvider>();
            services.Register(()=> new MailSenderDBDataContext());
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