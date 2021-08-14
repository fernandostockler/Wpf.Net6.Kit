using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Wpf.Net6.Kit.Controls.Shared;

namespace Wpf.Net6.Kit.Controls
{
    /// <summary>
    /// The SideMenu class represents a window with a left-hand menu for navigation and the remaining space for displaying content.
    /// </summary>
    [TemplatePart(Name = PART_GridSplitter, Type = typeof(GridSplitter))]
    [TemplatePart(Name = PART_LeftPanelColumn, Type = typeof(ColumnDefinition))]
    [TemplatePart(Name = PART_HamburgerButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_Frame, Type = typeof(Frame))]
    public class SideMenu : ListBox
    {
        private const string Comum = "Comum";
        private const string NoItemIsSelected = "NoItemIsSelected";
        private readonly TextBlock BackgroundPageTextBlock = new()
        {

            Text = "This will be displayed if no items are selected or if the Pages property does not contain any entries. Use BackgroundPage property to set this page with any FrameworkElement or set to null.",
            FontSize = 20,
            Foreground = Brushes.Black,
            Padding = new Thickness(50, 20, 50, 20),
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.Wrap
        };

        static SideMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(forType: typeof(SideMenu), typeMetadata: new FrameworkPropertyMetadata(typeof(SideMenu)));
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public SideMenu()
        {
            _ = CommandBindings.Add(new CommandBinding(
                command: TogglePanelCommand,
                executed: TogglePanelCommandExecuted,
                canExecute: TogglePanelCommandCanExecute));

            BackgroundPage = BackgroundPageTextBlock;
            Loaded += SideMenu_Loaded;
            SelectionChanged += SideMenu_SelectionChanged;
        }

        /// <summary>
        /// A RoutedCommand to open / close the side menu.
        /// </summary>
        public static readonly RoutedCommand TogglePanelCommand = new(
            name: nameof(TogglePanelCommand),
            ownerType: typeof(SideMenu), 
            inputGestures: new(new List<KeyGesture>() { new(Key.Space, ModifierKeys.Shift) }));

        private void TogglePanelCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void TogglePanelCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            double oldValue = MenuWidth;
            MenuWidth = MenuWidth > MenuMinWidth ? MenuMinWidth : ActualMenuMaxWidth;
            OnPropertyChanged(new(MenuWidthProperty, oldValue, MenuWidth));
            e.Handled = true;

            GridLengthAnimation? animation = new()
            {
                From = new GridLength(oldValue, GridUnitType.Pixel),
                To = new GridLength(MenuWidth, GridUnitType.Pixel),
                Duration = new(TimeSpan.FromSeconds(0.15)),
                AccelerationRatio = 0.4,
                DecelerationRatio = 0.6
            };

            Storyboard? storyboard = new();
            storyboard.Children.Add(animation);
            Storyboard.SetTargetName(animation, PartLeftPanelColumn.Name);
            PropertyPath propertyPath = new(ColumnDefinition.WidthProperty);
            Storyboard.SetTargetProperty(animation, propertyPath);
            storyboard.Begin(containingObject: PartLeftPanelColumn);
        }

        private void SideMenu_Loaded(object sender, RoutedEventArgs e)
        {
            SideMenuItem sideMenuItem = (SideMenuItem)SelectedItem;
            string pageTypeName = SelectedIndex > -1 ? sideMenuItem.PageTypeName : NoItemIsSelected;
            SelectPage(pageTypeName);
        }

        private void SideMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is SideMenuItem item)
            {
                SelectPage(item.PageTypeName);
            }
        }

        private void SelectPage(string pageTypeName)
        {
            if (IsLoaded)
            {
                bool hasNoSelectedItems = !string.IsNullOrEmpty(pageTypeName) && pageTypeName == NoItemIsSelected;
                PartFrame.Content = hasNoSelectedItems || Pages.Count == 0
                    ? BackgroundPage : Pages.ContainsKey(pageTypeName)
                    ? Pages[pageTypeName] : PageNotFoudedMessage;
            }
        }

        private void GridSplitter_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            double oldValue = MenuWidth;
            MenuWidth = oldValue + e.HorizontalChange;
            OnPropertyChanged(new(MenuWidthProperty, oldValue, MenuWidth));
            ActualMenuMaxWidth = MenuWidth;
            e.Handled = true;
        }

        /// <summary>
        /// Gets or sets the System.Collection.Generics.Dictionary{string, FrameworkElement} used for navigation.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets the System.Collection.Generics.Dictionary<string, FrameworkElement> used for navigation.")]
        public Dictionary<string, FrameworkElement> Pages
        {
            get => (Dictionary<string, FrameworkElement>)GetValue(PagesProperty);
            set => SetValue(PagesProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Pages"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="Pages"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty PagesProperty =
            DependencyProperty.Register(
                name: nameof(Pages),
                propertyType: typeof(Dictionary<string, FrameworkElement>),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: new Dictionary<string, FrameworkElement>()));

        /// <summary>
        /// Gets or sets the FrameworkElement that will be displayed if no items are selected or if the <see cref = "Pages" /> property contains no entries.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets the FrameworkElement that will be displayed if no items are selected or if the Pages property contains no entries.")]
        public FrameworkElement BackgroundPage
        {
            get => (FrameworkElement)GetValue(BackgroundPageProperty);
            set => SetValue(BackgroundPageProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="BackgroundPage"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="BackgroundPage"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty BackgroundPageProperty =
            DependencyProperty.Register(
                name: nameof(BackgroundPage),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(defaultValue: null));

        /// <summary>
        /// Gets or sets a FrameworkElement that will be displayed if the requested page does not have any matches on Pages
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a FrameworkElement that will be displayed if the requested page does not have any matches on Pages[].")]
        public FrameworkElement PageNotFoudedMessage
        {
            get => (FrameworkElement)GetValue(PageNotFoudedMessageProperty);
            set => SetValue(PageNotFoudedMessageProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="PageNotFoudedMessage"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="PageNotFoudedMessage"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty PageNotFoudedMessageProperty =
            DependencyProperty.Register(
                name: nameof(PageNotFoudedMessage),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(PageNotFounded));

        private static readonly TextBlock PageNotFounded = new()
        {
            Text = "Page not founded!",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 42.0
        };

        /// <summary>
        /// Gets or sets a brush representing the background color of the content area.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a brush representing the background color of the content area.")]
        public Brush ContentBackground
        {
            get => (Brush)GetValue(ContentBackgroundProperty);
            set => SetValue(ContentBackgroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ContentBackground"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="ContentBackground"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ContentBackgroundProperty =
            DependencyProperty.Register(
                name: nameof(ContentBackground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// Gets or sets a value representing the visibility of the frame's navigation bar.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a value representing the visibility of the frame's navigation bar.")]
        public NavigationUIVisibility NavigationUIVisibility
        {
            get => (NavigationUIVisibility)GetValue(NavigationUIVisibilityProperty);
            set => SetValue(NavigationUIVisibilityProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="NavigationUIVisibility"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="NavigationUIVisibility"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty NavigationUIVisibilityProperty =
            DependencyProperty.Register(
                name: nameof(NavigationUIVisibility),
                propertyType: typeof(NavigationUIVisibility),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(NavigationUIVisibility.Hidden));

        /// <summary>
        /// Gets or sets a custom FrameworkElement located at the bottom left of the control.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a custom FrameworkElement located at the bottom left of the control.")]
        public FrameworkElement CustomArea
        {
            get => (FrameworkElement)GetValue(CustomAreaProperty);
            set => SetValue(CustomAreaProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="CustomArea"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="CustomArea"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty CustomAreaProperty =
            DependencyProperty.Register(
                name: nameof(CustomArea),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        /// <summary>
        /// Gets or sets a value representing the always visible portion of the left pane.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a value representing the always visible portion of the left pane.")]
        public double MenuMinWidth
        {
            get => (double)GetValue(MenuMinWidthProperty);
            set => SetValue(MenuMinWidthProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="MenuMinWidth"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MenuMinWidth"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty MenuMinWidthProperty =
            DependencyProperty.Register(
                name: nameof(MenuMinWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 55.0));

        /// <summary>
        /// Gets or sets a value representing the maximum width of the left pane.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a value representing the maximum width of the left pane.")]
        public double MenuMaxWidth
        {
            get => (double)GetValue(MenuMaxWidthProperty);
            set => SetValue(MenuMaxWidthProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="MenuMaxWidth"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MenuMaxWidth"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty MenuMaxWidthProperty =
            DependencyProperty.Register(
                name: nameof(MenuMaxWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 320.0));

        /// <summary>
        /// Gets or sets a value representing the width of the left pane.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a value representing the width of the left pane.")]
        public double MenuWidth
        {
            get => (double)GetValue(MenuWidthProperty);
            set => SetValue(MenuWidthProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="MenuWidth"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="MenuWidth"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty MenuWidthProperty =
            DependencyProperty.Register(
                name: nameof(MenuWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 320.0));

        /// <summary>
        /// Gets or sets a brush that describes the foreground color of the button that opens/closes the left panel.
        /// </summary>
        [Description("Gets or sets a brush that describes the foreground color of the button that opens/closes the left panel.")]
        public Brush HamburgerMenuForeground
        {
            get => (Brush)GetValue(HamburgerMenuForegroundProperty);
            set => SetValue(HamburgerMenuForegroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="HamburgerMenuForeground"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="HamburgerMenuForeground"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty HamburgerMenuForegroundProperty =
            DependencyProperty.Register(
                name: nameof(HamburgerMenuForeground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.GreenYellow));

        /// <summary>
        /// Gets or sets a brush that describes the color of the GridSplitter.
        /// </summary>
        [Description("Gets or sets a brush that describes the color of the GridSplitter.")]
        public Brush GridSplitterBackground
        {
            get => (Brush)GetValue(GridSplitterBackgroundProperty);
            set => SetValue(GridSplitterBackgroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="GridSplitterBackground"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="GridSplitterBackground"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty GridSplitterBackgroundProperty =
            DependencyProperty.Register(
                name: nameof(GridSplitterBackground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.Transparent));

        /// <summary>
        /// Gets or sets a value representing the thickness of the GridSplitter.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a value representing the thickness of the GridSplitter.")]
        public double GridSplitterWidth
        {
            get => (double)GetValue(GridSplitterWidthProperty);
            set => SetValue(GridSplitterWidthProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="GridSplitterBackground"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="GridSplitterBackground"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty GridSplitterWidthProperty =
            DependencyProperty.Register(
                name: nameof(GridSplitterWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 1.0));

        /// <summary>
        /// Gets or sets a value representing whether GridSplitter is enabled.
        /// </summary>
        [Category(Comum)]
        [Description("Gets or sets a value representing whether GridSplitter is enabled.")]
        public bool GridSplitterIsEnabled
        {
            get => (bool)GetValue(GridSplitterIsEnabledProperty);
            set => SetValue(GridSplitterIsEnabledProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="GridSplitterIsEnabled"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="GridSplitterIsEnabled"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty GridSplitterIsEnabledProperty =
            DependencyProperty.Register(
                name: nameof(GridSplitterIsEnabled),
                propertyType: typeof(bool),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: false));


        private T GetTemplateChild<T>(string childName) where T : DependencyObject
        {
            T child = (T)GetTemplateChild(childName);
            return child is null ? throw new MissingTemplatePartException(childName, typeof(T)) : child;
        }

        /// <summary>
        /// Builds the visual tree for the control when a new template is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PartFrame = GetTemplateChild<Frame>(PART_Frame);
            PartLeftPanelColumn = GetTemplateChild<ColumnDefinition>(PART_LeftPanelColumn);
            PartGridSplitter = GetTemplateChild<GridSplitter>(PART_GridSplitter);
            PartGridSplitter.DragCompleted -= GridSplitter_DragCompleted;
            PartGridSplitter.DragCompleted += GridSplitter_DragCompleted;
        }

        /// <summary>
        /// Creates or identifies the element used to display a specified item
        /// </summary>
        /// <returns>
        /// A SideMenuItem.
        /// </returns>
        protected override DependencyObject GetContainerForItemOverride() => new SideMenuItem();

        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own ItemContainer.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// A SideMenuItem.
        /// </returns>
        protected override bool IsItemItsOwnContainerOverride(object item) => item is SideMenuItem;

        private const string PART_GridSplitter = "PART_GridSplitter";
        private const string PART_LeftPanelColumn = "PART_LeftPanelColumn";
        private const string PART_HamburgerButton = "PART_HamburgerButton";
        private const string PART_Frame = "PART_Frame";

        internal GridSplitter PartGridSplitter { get; set; } = new();
        internal ColumnDefinition PartLeftPanelColumn { get; set; } = new();
        internal Frame PartFrame { get; set; } = new();
        internal double ActualMenuMaxWidth { get; set; } = 320.0;
    }
}