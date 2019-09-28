using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSenderLib.MVVM
{
    public abstract class ViewModelZero : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string PropertyName=null) => //устанавливаем атрибут и значение по умолчанию, чтобы при вызове мотода без парамера, то в переменную ProperName будет автоматически передаваться имя метода, из которого был вызван данный метод, вт.ч. и имя свойства
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        //упростит установку полей свойств. парамеьр  PropertyName при этом определяется автоматически
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName]string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
