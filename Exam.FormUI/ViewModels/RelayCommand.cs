using System;
using System.Windows.Input;

namespace Exam.FormUI.ViewModels
{
    /// <summary>
    /// Класс необходимый для описания собственных команд.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Func<object, bool> func;

        public RelayCommand(Action<object> action, Func<object, bool> func = null)
        {
            this.action = action;
            this.func = func;
        }

        public bool CanExecute(object parameter)
        {
            return func == null || func(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
