using System;
using System.Windows.Input;

namespace WpfMvvmSkeleton.Core
{
    // Wraps any Action into an ICommand so it can be bound
    // directly to a Button (or any other control) in XAML.
    // canExecute is optional — if omitted the command is always enabled.
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add    { CommandManager.RequerySuggested += value;  }
            remove { CommandManager.RequerySuggested -= value;  }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute    = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) =>
            _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) =>
            _execute(parameter);
    }
}
