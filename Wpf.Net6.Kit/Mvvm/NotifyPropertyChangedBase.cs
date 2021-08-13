using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wpf.Net6.Kit.Mvvm
{
    /// <summary>
    /// A base class that notifies any change in a object's properties values.
    /// </summary>
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging"/>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">An instance of <see cref="PropertyChangedEventArgs"/>.</param>
        protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = "") => NotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Raises the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// <param name="e">An instance of <see cref="PropertyChangingEventArgs"/>.</param>
        protected virtual void NotifyPropertyChanging(PropertyChangingEventArgs e) => PropertyChanging?.Invoke(this, e);

        /// <summary>
        /// Raises the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// /// <param name="propertyName">(optional) The name of the property that is changing.</param>
        protected virtual void NotifyPropertyChanging([CallerMemberName] string? propertyName = "") => NotifyPropertyChanging(new PropertyChangingEventArgs(propertyName));
    }
}