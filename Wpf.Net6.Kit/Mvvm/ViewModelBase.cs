using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Wpf.Net6.Kit.Mvvm
{
    public abstract class ViewModelBase : NotifyPropertyChangedBase
    {
        protected ViewModelBase() { }

        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected virtual bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string? propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, newValue))
            {
                return false;
            }

            NotifyPropertyChanging(propertyName);

            storage = newValue;

            NotifyPropertyChanged(propertyName);

            return true;
        }


        protected virtual bool SetProperty<T>(ref T storage, T newValue, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null)
        {
            if (comparer.Equals(storage, newValue))
            {
                return false;
            }

            NotifyPropertyChanging(propertyName);

            storage = newValue;

            NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}