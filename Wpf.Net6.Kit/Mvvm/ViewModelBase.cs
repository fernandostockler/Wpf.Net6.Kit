using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Wpf.Net6.Kit.Mvvm
{
    /// <summary>
    /// A base class for viewmodels that allows their properties to be observable.
    /// </summary>
    public abstract class ViewModelBase : NotifyPropertyChangedBase
    {
        protected ViewModelBase() { }

        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}