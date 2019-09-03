using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ProductionScheduler.Services {
    internal class RelayCommand<T> : ICommand {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute) :
            this(execute, null) { }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null) {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /*
         * Called every time user input is detected. Can cause performance issues
         * if CanExecute has complicated logic in it.
        */

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter) {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter) {
            _execute((T)parameter);
        }
    }
}
