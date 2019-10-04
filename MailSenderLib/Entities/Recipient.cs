using MailSenderLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities
{
    public class Recipient : HumanEntity, IDataErrorInfo // интерфейс  - если какое-то св-во (#Name) имеет ошибку, то внешняя система, которая занимается обработкой информации этого св-ва передает имя свойства в Индексатор. Если Инексатором будет возвращена строка, отличная от указанного в нем условия валидности (#пустая строка), то она и будет сообщать об ошибочном состоянии св-ва, а иначе - св-во валидно 
    {

        public override string Name { get => base.Name; set => base.Name = value; }

        private string _description;

        public string Description // еще один способ валидации данных через сам сеттер
        {
            get => _description;
            set
            {
                if (value.IndexOf('@') != -1) throw new ArgumentOutOfRangeException(nameof(value), "Описание не может содержать @");
                _description = value;
            }
}

//public string Error => "";//в составе интерфейса обработчика ошибок. Если ошибка на уровне всей модел (некоторая комбинация св-в некорректна), то внешнюю систему можно изместить об этом, возвращая в Error строку отличную от пустой 
string IDataErrorInfo.Error => "";//закрываем доступность для тех, кто занимается обработкой данных Реципента, чтобы были видны только для тех, кто работает с валидностью

        //public string this[string columnName] => "";//Индексатор в составе интерфейса обработчика ошибок, может принимать имена свойств
        string IDataErrorInfo.this[string PropertyName] //аналогично
        {
            get
            {
                switch (PropertyName)
                {
                    default: return string.Empty;
                    case nameof(Name):
                        if (Name is null) return "Отсуствует ссылка на сроку с именем";
                        if (Name.Length <= 3) return "Имя должно быть длиннее 3 символов";
                        if (!char.IsLetter(Name[0])) return "Имя должно начинаться с буквы";
                        return string.Empty;
                }
            }
        }
    }
}
