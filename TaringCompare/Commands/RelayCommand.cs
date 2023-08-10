using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaringCompare.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> executionAction)
        {
            _executeAction = executionAction;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _executeAction(parameter);

        public event EventHandler CanExecutedChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
    }
}
