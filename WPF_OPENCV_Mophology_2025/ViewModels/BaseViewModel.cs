using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_OPENCV_Morphology_2025.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class RelayCommand<T> : ICommand
    {
        Predicate<T> _CanExecute;
        Action<T> _Execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            _CanExecute = canExecute;
            _Execute = execute;
        }

        event EventHandler ICommand.CanExecuteChanged
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

        bool ICommand.CanExecute(object parameter)
        {
            return this._CanExecute==null?true:this._CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this._Execute((T)parameter);
        }
    }
}
