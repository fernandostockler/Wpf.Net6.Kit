﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// This file was light modified from WindowsCommunityToolkit/Microsoft.Toolkit.Mvvm/Input/RelayCommand{T}.cs

using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Wpf.Net6.Kit.Mvvm
{
    /// <summary>
    /// A generic command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is <see langword="true"/>. This class allows you to accept command parameters
    /// in the <see cref="Execute(T)"/> and <see cref="CanExecute(T)"/> callback methods.
    /// </summary>
    /// <typeparam name="T">The type of parameter being passed as input to the callbacks.</typeparam>
    public sealed class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// The <see cref="Action"/> to invoke when <see cref="Execute(T)"/> is used.
        /// </summary>
        private readonly Action<T?> execute;

        /// <summary>
        /// The optional action to invoke when <see cref="CanExecute(T)"/> is used.
        /// </summary>
        private readonly Predicate<T?>? canExecute;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <remarks>
        /// Due to the fact that the <see cref="System.Windows.Input.ICommand"/> interface exposes methods that accept a
        /// nullable <see cref="object"/> parameter, it is recommended that if <typeparamref name="T"/> is a reference type,
        /// you should always declare it as nullable, and to always perform checks within <paramref name="execute"/>.
        /// </remarks>
        public RelayCommand(Action<T?> execute) => this.execute = execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <remarks>See notes in <see cref="RelayCommand{T}(Action{T})"/>.</remarks>
        public RelayCommand(Action<T?> execute, Predicate<T?> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <inheritdoc/>
        public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanExecute(T? parameter) => canExecute?.Invoke(parameter) != false;

        /// <inheritdoc/>
        public bool CanExecute(object? parameter) => (default(T) is null || parameter is not null) && CanExecute((T?)parameter);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(T? parameter)
        {
            if (CanExecute(parameter)) execute(parameter);
        }

        /// <inheritdoc/>
        public void Execute(object? parameter) => Execute((T?)parameter);
    }
}