using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.Net6.Kit.Mvvm
{
    public class RelayCommand<T> : ICommand
    {
        public Func<T, bool>? CanExecuteDelegate { get; set; }
        public Action<T>? ExecuteDelegate { get; set; }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => CanExecuteDelegate is null || CanExecuteDelegate((T)parameter);

        public void Execute(object? parameter) => ExecuteDelegate((T)parameter);

        public RelayCommand(Func<T, bool>? canExecute = null, Action<T>? execute = null)
        {
            CanExecuteDelegate = canExecute;
            ExecuteDelegate = execute;
        }
    }
}