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

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}