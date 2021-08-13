using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wpf.Net6.Kit.Mvvm
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event PropertyChangingEventHandler? PropertyChanging;

        protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = "") => NotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));

        protected virtual void NotifyPropertyChanging(PropertyChangingEventArgs e) => PropertyChanging?.Invoke(this, e);

        protected virtual void NotifyPropertyChanging([CallerMemberName] string? propertyName = "") => NotifyPropertyChanging(new PropertyChangingEventArgs(propertyName));
    }
}