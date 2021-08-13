using System.Collections.Generic;
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

        /// <summary>
        /// Compares the property's stored value with the new entered value. 
        /// If they are equal, false is returned, otherwise the stored value is updated with the new value, 
        /// PropertyChanging, PropertyChanged events are raised, and true is returned.
        /// </summary>
        /// <typeparam name="T">The type of property that is changing.</typeparam>
        /// <param name="storage">The currently stored value.</param>
        /// <param name="newValue">The updated value for this property.</param>
        /// <param name="propertyName">The property name (optional).</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
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

        /// <summary>
        /// Compares the property's stored value with the new entered value. 
        /// If they are equal, false is returned, otherwise the stored value is updated with the new value, 
        /// PropertyChanging, PropertyChanged events are raised, and true is returned.
        /// </summary>
        /// <typeparam name="T">The type of property that is changing.</typeparam>
        /// <param name="storage">The currently stored value.</param>
        /// <param name="newValue">The updated value for this property.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> instance to use to compare the input values.</param>
        /// <param name="propertyName">The property name (optional).</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
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