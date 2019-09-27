using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSenderLib.MVVM;

namespace MailSender
{
    class PannelsViewModel: ViewModel
    {
        //основыные св-ва нашей и\вью-модели
        private string _Title = "Name of window";

        public string Title
        {
            get => _Title;
            set
            {
                if (_Title == value) return;//если значение поля не меняется - ничего не делаем, иначе меняем
                _Title = value;
                //OnPropertyChanged(nameof(Title));
                //здесь же можно менять и другие св-ва
                //OnPropertyChanged(Title); можно упростить, убрав параметр-ссылку на само поле
                OnPropertyChanged();
                //OnPropertyChanged("TitleLength"); можно иначе
                OnPropertyChanged(nameof(TitleLength));
            }
        }

        public int TitleLength => Title?.Length ?? 0;

        private int _OffsetX = 10;

        public int OffsetX
        {
            get => OffsetX;
            set => Set(ref _OffsetX, value);
        }
        private int _OffsetY = 10;

        public int OffsetY
        {
            get => OffsetY;
            set => Set(ref _OffsetY, value);
        }
        private double _Angle = 10;

        public double Angle
        {
            get => Angle;
            set => Set(ref _Angle, value);
        }
    }
}
