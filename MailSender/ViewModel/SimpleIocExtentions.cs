using System;
using GalaSoft.MvvmLight.Ioc;

namespace MailSender.ViewModel
{
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