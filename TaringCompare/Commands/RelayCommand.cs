using System;
using System.Windows.Input;

namespace TaringCompare.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object,bool> _canExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> executionAction, Func<object,bool> canExecute = null)
        {
            _executeAction = executionAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object? parameter) => _executeAction(parameter);

        public event EventHandler CanExecutedChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
