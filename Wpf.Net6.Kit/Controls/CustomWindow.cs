﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using Wpf.Net6.Kit.Controls.Shared;

namespace Wpf.Net6.Kit.Controls
{
    [TemplatePart(Name = PART_Icon, Type = typeof(ContentControl))]
    [TemplatePart(Name = PART_LeftArea, Type = typeof(ContentControl))]
    [TemplatePart(Name = PART_Title, Type = typeof(ContentControl))]
    [TemplatePart(Name = PART_RightArea, Type = typeof(ContentControl))]
    [TemplatePart(Name = PART_MinimizeButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_MaximizeRestoreButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(ButtonBase))]
    public partial class CustomWindow : Window
    {
        static CustomWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        public CustomWindow()
        {
            _ = CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
            _ = CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeRestoreWindow, CanResizeWindow));
            _ = CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, MaximizeRestoreWindow, CanResizeWindow));
            _ = CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow, CanMinimizeWindow));

            StateChanged += CustomWindow_StateChanged;
            Loaded += CustomWindow_Loaded;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            CheckKioskExitKeyGesture(e);

            void CheckKioskExitKeyGesture(KeyEventArgs e)
            {
                KioskExitKeyGesture k = KioskModeExitKeyGesture;
                bool match = KioskModeExitKeyGesture.ModifierKeys.Length switch
                {
                    1 => k.ModifierKeys[0] == (e.KeyboardDevice.Modifiers & k.ModifierKeys[0]),

                    2 => k.ModifierKeys[0] == (e.KeyboardDevice.Modifiers & k.ModifierKeys[0]) &&
                         k.ModifierKeys[1] == (e.KeyboardDevice.Modifiers & k.ModifierKeys[1]),

                    3 => k.ModifierKeys[0] == (e.KeyboardDevice.Modifiers & k.ModifierKeys[0]) &&
                         k.ModifierKeys[1] == (e.KeyboardDevice.Modifiers & k.ModifierKeys[1]) &&
                         k.ModifierKeys[2] == (e.KeyboardDevice.Modifiers & k.ModifierKeys[2]),

                    /// Unselect the 3 lines below if you need to alert that only 3 first items will be processed.
                    //> 3 => throw new ArrayExceedsMaximumLengthException(
                    //       arrayName: "KioskExitKeyGesture.ModifierKeys[ ]",
                    //       message: $"There are {k.ModifierKeys.Length} items in KioskModeExitKeyGesture.ModifierKeys[ ] array. It can hold only 3 items."),

                    _ => false,
                };
                if (match && (e.Key == KioskModeExitKeyGesture.Key || e.SystemKey == KioskModeExitKeyGesture.Key))
                {
                    e.Handled = true;
                    KioskMode = false;
                }
            }
        }

        private void CustomWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OnKioskModeChanged(this, new(KioskModeProperty, KioskMode, KioskMode));
            OnTitleBarHeightChanged(this, new(TitleBarHeightProperty, TitleBarHeight, TitleBarHeight));

            // if CustomDialog property has not been set in code or xaml, it will be set by GetDefaultCustomDialog().
            // you can comment the next line if you don't want this behavior.
            CustomDialog ??= GetDefaultCustomDialog();

            StackPanel GetDefaultCustomDialog()
            {
                StackPanel stackPanel = new()
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(20)
                };
                TextBlock textBlock = new()
                {
                    Text = "Hello!",
                    FontSize = 30,
                    Foreground = Brushes.Violet,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                TextBlock textBlock1 = new()
                {
                    Text = "This is just an example of a CustomDialog. " +
                           "Use the CustomDialog property to define a custom CustomDialog with any FrameworkElement. " +
                           "To display it just set the ShowCustomDialog property to True. " +
                           "If no CustomDialog is set, this message will be showed.",
                    FontSize = 14,
                    Margin = new Thickness(20),
                    Foreground = Brushes.Silver,
                    TextWrapping = TextWrapping.Wrap,
                    TextTrimming = TextTrimming.WordEllipsis,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Button button = new()
                {
                    Content = "Enter",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.Silver,
                    Background = Brushes.Transparent,
                    Padding = new Thickness(10, 5, 10, 5),
                    Margin = new Thickness(0, 10, 0, 0)
                };
                button.Click += CustomDialogEnterButton_Click; ;
                _ = stackPanel.Children.Add(textBlock);
                _ = stackPanel.Children.Add(textBlock1);
                _ = stackPanel.Children.Add(button);
                return stackPanel;
            }
        }

        /// <summary>
        /// Sets CustomDialog visibility to Visibility.Collapsed
        /// </summary>
        /// <remarks>This event handler is only used thru GetDefaultCustomDialog() function.</remarks>
        private void CustomDialogEnterButton_Click(object sender, RoutedEventArgs e)
        {
            ShowCustomDialog = false;
        }

        /// <summary=> Shadows the WindowStyle property to prevent it from being changed from WindowStyle.None .
        /// <remarks=> Any attempt to modify this property will launch an exception:
        /// <exception=> cref="Error MC3080  The property 'CustomWindow.WindowStyle' cannot be set because it does not have an accessible set accessor."
        public new WindowStyle WindowStyle
        {
            get => (WindowStyle)GetValue(WindowStyleProperty);
            private set => SetValue(WindowStyleProperty, value);
        }

        /// <summary=> Shadows the AllowsTransparency property to prevent it from being changed from AllowTransparency = True.
        /// <remarks=> Any attempt to modify this property will launch an exception:
        /// <exception=> cref="Error MC3080  The property 'CustomWindow.AllowsTransparency' cannot be set because it does not have an accessible set accessor."
        public new bool AllowsTransparency
        {
            get => (bool)GetValue(AllowsTransparencyProperty);
            private set => SetValue(AllowsTransparencyProperty, value);
        }

        [Category(CustomWindowCategory)]
        [Description("Gets or sets a Boolean value representing whether KioskMode is turned on/off.")]
        public bool KioskMode
        {
            get => (bool)GetValue(KioskModeProperty);
            set => SetValue(KioskModeProperty, value);
        }
        public static readonly DependencyProperty KioskModeProperty =
            DependencyProperty.Register(
                name: nameof(KioskMode),
                propertyType: typeof(bool),
                ownerType: typeof(CustomWindow),
                typeMetadata: new FrameworkPropertyMetadata(false,
                    propertyChangedCallback: OnKioskModeChanged));

        private static double OriginalTitleBarHeight = 42.0;

        private static void OnKioskModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomWindow window = (CustomWindow)d;
            bool value = (bool)e.NewValue;
            if (value)
            {
                OriginalTitleBarHeight = window.TitleBarHeight;
                window.MinTitleBarHeight = 0.0;
                window.TitleBarHeight = 0.0;
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.MinTitleBarHeight = 36.0;
                window.TitleBarHeight = OriginalTitleBarHeight;
                window.WindowState = WindowState.Maximized;
            }
        }

        [Category(CustomWindowCategory)]
        [Description("Gets or sets a key combination that turns off kiosk mode.")]
        public KioskExitKeyGesture KioskModeExitKeyGesture
        {
            get => (KioskExitKeyGesture)GetValue(KioskModeExitKeyGestureProperty);
            set => SetValue(KioskModeExitKeyGestureProperty, value);
        }
        public static readonly DependencyProperty KioskModeExitKeyGestureProperty =
            DependencyProperty.Register(
                name: nameof(KioskModeExitKeyGesture),
                propertyType: typeof(KioskExitKeyGesture),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: new KioskExitKeyGesture(Key.End, new ModifierKeys[] { ModifierKeys.Shift, ModifierKeys.Alt })));

        public DataTemplate IconTemplate
        {
            get => (DataTemplate)GetValue(IconTemplateProperty);
            set => SetValue(IconTemplateProperty, value);
        }
        public static readonly DependencyProperty IconTemplateProperty =
            DependencyProperty.Register(
                name: nameof(IconTemplate),
                propertyType: typeof(DataTemplate),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(CustomWindowCategory)]
        public FrameworkElement IconArea
        {
            get => (FrameworkElement)GetValue(IconAreaProperty);
            set => SetValue(IconAreaProperty, value);
        }
        public static readonly DependencyProperty IconAreaProperty =
            DependencyProperty.Register(
                name: nameof(IconArea),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(CustomWindowCategory)]
        public double MinTitleBarHeight
        {
            get => (double)GetValue(MinTitleBarHeightProperty);
            set => SetValue(MinTitleBarHeightProperty, value);
        }
        public static readonly DependencyProperty MinTitleBarHeightProperty =
            DependencyProperty.Register(
                name: nameof(MinTitleBarHeight),
                propertyType: typeof(double),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(36.0));

        [Category(CustomWindowCategory)]
        [Description("Gets or sets the height of the title bar (non-client area).")]
        public double TitleBarHeight
        {
            get => (double)GetValue(TitleBarHeightProperty);
            set => SetValue(TitleBarHeightProperty, value);
        }
        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register(
                name: nameof(TitleBarHeight),
                propertyType: typeof(double),
                ownerType: typeof(CustomWindow),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: 42.0,
                    propertyChangedCallback: OnTitleBarHeightChanged,
                    coerceValueCallback: OnCoerceTitleBarHeight),
                    validateValueCallback: OnValidateTitleBarHeight);

        private static object OnCoerceTitleBarHeight(DependencyObject d, object baseValue)
        {
            CustomWindow customWindow = (CustomWindow)d;
            return (baseValue is double value && value < customWindow.MinTitleBarHeight) ? customWindow.MinTitleBarHeight : baseValue;
        }

        private static bool OnValidateTitleBarHeight(object value)
        {
            return value switch
            {
                double dvalue => !double.IsNaN(dvalue) && !double.IsNegativeInfinity(dvalue) && !double.IsPositiveInfinity(dvalue),
                _ => false
            };
        }

        protected const int ResizeBorderThicknessDefaultValue = 6;

        private static void OnTitleBarHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomWindow? win = (CustomWindow)d;
            double newValue = (double)e.NewValue;
            WindowChrome.SetWindowChrome(win, new WindowChrome()
            {
                CaptionHeight = newValue,
                CornerRadius = new CornerRadius(0),
                ResizeBorderThickness = new Thickness(ResizeBorderThicknessDefaultValue)
            });
        }

        [Category(CustomWindowCategory)]
        [Description("Gets or sets the FrameworkElement located in the left part of the title bar, just after the window icon.")]
        public FrameworkElement TitleBarLeftArea
        {
            get => (FrameworkElement)GetValue(TitleBarLeftAreaProperty);
            set => SetValue(TitleBarLeftAreaProperty, value);
        }
        public static readonly DependencyProperty TitleBarLeftAreaProperty =
            DependencyProperty.Register(
                name: nameof(TitleBarLeftArea),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(CustomWindowCategory)]
        [Description("Gets or sets the FrameworkElement located in the right part of the title bar, before the window buttons.")]
        public FrameworkElement TitleBarRightArea
        {
            get => (FrameworkElement)GetValue(TitleBarRightAreaProperty);
            set => SetValue(TitleBarRightAreaProperty, value);
        }
        public static readonly DependencyProperty TitleBarRightAreaProperty =
            DependencyProperty.Register(
                name: nameof(TitleBarRightArea),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(CustomWindowCategory)]
        [Description("Obtem ou define um pincel que descreve a cor do primeiro plano da barra de título da janela.")]
        internal Brush TitleBarForeground
        {
            get => (Brush)GetValue(TitleBarForegroundProperty);
            set => SetValue(TitleBarForegroundProperty, value);
        }
        public static readonly DependencyProperty TitleBarForegroundProperty =
            DependencyProperty.Register(
                name: nameof(TitleBarForeground),
                propertyType: typeof(Brush),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.Black));

        [Category(CustomWindowCategory)]
        [Description("Gets or sets the brush that describes the background of the window's title bar.")]
        public Brush TitleBarBackground
        {
            get => (Brush)GetValue(TitleBarBackgroundProperty);
            set => SetValue(TitleBarBackgroundProperty, value);
        }
        public static readonly DependencyProperty TitleBarBackgroundProperty =
            DependencyProperty.Register(
                name: nameof(TitleBarBackground),
                propertyType: typeof(Brush),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.White,
                    propertyChangedCallback: OnTitleBarBackgroundChanged));

        private static void OnTitleBarBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomWindow? win = (CustomWindow)d;
            Brush? newValue = (Brush)e.NewValue;
            BackgroundToForegroundConverter? converter = BackgroundToForegroundConverter.Instance;
            Brush? newIdealForeground = converter.Convert(newValue, typeof(Brush), new object(), CultureInfo.CurrentCulture) as Brush;
            win.TitleBarForeground = newIdealForeground ?? SystemColors.HotTrackBrush;
        }

        [Category(CustomWindowCategory)]
        [Description("Gets or sets the DataTemplate for the Title property.")]
        public DataTemplate TitleTemplate
        {
            get => (DataTemplate)GetValue(TitleTemplateProperty);
            set => SetValue(TitleTemplateProperty, value);
        }
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register(
                name: nameof(TitleTemplate),
                propertyType: typeof(DataTemplate),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(CustomWindowCategory)]
        [Description("Obtem ou define um pincel que representa o plano de fundo da camada que cobre a janela.")]
        public Brush OverlayBackground
        {
            get => (Brush)GetValue(OverlayBackgroundProperty);
            set => SetValue(OverlayBackgroundProperty, value);
        }
        public static readonly DependencyProperty OverlayBackgroundProperty =
            DependencyProperty.Register(
                name: nameof(OverlayBackground),
                propertyType: typeof(Brush),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.Gray));

        [Category(CustomWindowCategory)]
        [Description("Obtem ou define a visibilidade da camada que cobre a janela.")]
        public bool ShowCustomDialog
        {
            get => (bool)GetValue(ShowCustomDialogProperty);
            set => SetValue(ShowCustomDialogProperty, value);
        }
        public static readonly DependencyProperty ShowCustomDialogProperty =
            DependencyProperty.Register(
                name: nameof(ShowCustomDialog),
                propertyType: typeof(bool),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: false));

        [Category(CustomWindowCategory)]
        public FrameworkElement CustomDialog
        {
            get => (FrameworkElement)GetValue(CustomDialogProperty);
            set => SetValue(CustomDialogProperty, value);
        }
        public static readonly DependencyProperty CustomDialogProperty =
            DependencyProperty.Register(
                name: nameof(CustomDialog),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(CustomWindowCategory)]
        public Brush CustomDialogBackground
        {
            get => (Brush)GetValue(CustomDialogBackgroundProperty);
            set => SetValue(CustomDialogBackgroundProperty, value);
        }
        public static readonly DependencyProperty CustomDialogBackgroundProperty =
            DependencyProperty.Register(
                name: nameof(CustomDialogBackground),
                propertyType: typeof(Brush),
                ownerType: typeof(CustomWindow),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.DarkBlue));

        private static readonly Thickness NormalThickness = new(0);
        private static double WindowsTaskbarHeight => SystemParameters.PrimaryScreenHeight - SystemParameters.FullPrimaryScreenHeight - SystemParameters.WindowCaptionHeight;
        private static double WindowsTaskbarWidth => SystemParameters.PrimaryScreenWidth - SystemParameters.FullPrimaryScreenWidth;

        private void CustomWindow_StateChanged(object? sender, EventArgs e)
        {
            bool WindowStateIsNormal = WindowState == WindowState.Normal;
            MaximizeRestoreButton.Content = WindowStateIsNormal ? RestoreGlyph : MaximizeGlyph;
            MaximizeRestoreButton.ToolTip = WindowStateIsNormal ? MaximizeToolTip : RestoreToolTip;
            // Fixes the issue that occurs when the window is maximized
            Margin = WindowState == WindowState.Maximized ? GetMaximazedThickness() : NormalThickness;

            Thickness GetMaximazedThickness()
            {
                Thickness resizeBorderThickness = WindowChrome.GetWindowChrome(this).ResizeBorderThickness;

                Thickness thickness = new(
                    left: resizeBorderThickness.Left + BorderThickness.Left,
                     top: resizeBorderThickness.Top + BorderThickness.Top,
                   right: resizeBorderThickness.Right + BorderThickness.Right,
                  bottom: resizeBorderThickness.Bottom + BorderThickness.Bottom);

                switch (GetTaskbarPosition())
                {
                    case TaskbarPosition.Top:
                        thickness.Top += WindowsTaskbarHeight;
                        break;
                    case TaskbarPosition.Left:
                        thickness.Left += WindowsTaskbarWidth;
                        break;
                    case TaskbarPosition.Bottom:
                        thickness.Bottom += WindowsTaskbarHeight;
                        break;
                    case TaskbarPosition.Right:
                        thickness.Right += WindowsTaskbarWidth;
                        break;
                    case TaskbarPosition.None:
                        break;
                    default:
                        break;
                }
                return thickness;
            }

            TaskbarPosition GetTaskbarPosition()
            {
                if (SystemParameters.WorkArea.Top > 0)
                {
                    return TaskbarPosition.Top;
                }
                else if (SystemParameters.WorkArea.Left > 0)
                {
                    return TaskbarPosition.Left;
                }
                else if (WindowsTaskbarWidth == 0)
                {
                    return TaskbarPosition.Bottom;
                }
                else if (WindowsTaskbarHeight < WindowChrome.GetWindowChrome(this).CaptionHeight)
                {
                    return TaskbarPosition.Right;
                }
                return TaskbarPosition.None;
            }
        }

        private enum TaskbarPosition
        {
            None,
            Top,
            Left,
            Bottom,
            Right
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void CanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode is ResizeMode.CanResize or ResizeMode.CanResizeWithGrip;
        }

        private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void MaximizeRestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is Window window && window != null)
            {
                window.WindowState = (window.WindowState == WindowState.Normal)
                    ? WindowState.Maximized
                    : WindowState.Normal;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MaximizeRestoreButton = GetTemplateChild<Button>(PART_MaximizeRestoreButton);
        }

        public T GetTemplateChild<T>(string childName) where T : DependencyObject
        {
            T child = (T)GetTemplateChild(childName);
            return child is null ? throw new MissingTemplatePartException(childName, typeof(T)) : child;
        }

        internal Button MaximizeRestoreButton { get; private set; } = new();
        internal Border OutterBorder { get; private set; } = new();

        private const string MaximizeGlyph = "\uE923";
        private const string RestoreGlyph = "\uE922";
        private const string MaximizeToolTip = "Maximizar";
        private const string RestoreToolTip = "Restaurar";
        private const string CustomWindowCategory = nameof(CustomWindow);

        private const string PART_Icon = "PART_Icon";
        private const string PART_Title = "PART_Title";
        private const string PART_LeftArea = "PART_LeftArea";
        private const string PART_RightArea = "PART_RightArea";
        private const string PART_MinimizeButton = "PART_MinimizeButton";
        private const string PART_MaximizeRestoreButton = "PART_MaximizeRestoreButton";
        private const string PART_CloseButton = "PART_CloseButton";

        [Serializable]
        private class ArrayExceedsMaximumLengthException : Exception
        {
            private string? arrayName = string.Empty;
            public ArrayExceedsMaximumLengthException(string? arrayName, string? message) : base(message)
            {
                this.arrayName = arrayName;
            }
            protected ArrayExceedsMaximumLengthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
    }
}