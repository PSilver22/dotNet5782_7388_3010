#nullable enable
using System;
using System.Windows.Input;

namespace PL.Utilities
{
    public class DelegateCommand: ICommand
    {
        private readonly Predicate<object?>? canExecute;
        private readonly Action<object?> execute;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            execute?.Invoke(parameter);
        }


    }
}
