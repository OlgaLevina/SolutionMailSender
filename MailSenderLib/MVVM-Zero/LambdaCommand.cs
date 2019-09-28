using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailSenderLib.MVVM
{
    public class LambdaCommand : ICommand
    {
        public event EventHandler CanExecuteChanged //отслеживает момент перехода состояния команды из "нельзя выполнить" в "можно выполнить". данное событие можно реализовать руками, или при помощи коммандменеджера (бибиотека PresentationCore)
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        //ниже 2 метода - 1) проверяет можно ли команду выполнить или нет, 2) выполнять действие, предназначенное команде
        // приватные поля для сохранения результатов работы конструктора ниже
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute=null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public bool CanExecute(object parameter) => //проверяет можно ли выполнить команду или нет
            _CanExecute?.Invoke(parameter) ?? true; // если метод существует, то он будет вызван и его результат будет возвращен в качестве результата CanExecute, если же не существует - о считается, что команда может быть выполнена для любых параментров

        public void Execute(object parameter) => Execute(parameter); // выполняет команду
    }
}
