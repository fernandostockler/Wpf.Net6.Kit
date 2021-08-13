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

        [Category(Comum)]
        [Description("Obtem ou define um dicionário com as páginas (qualquer FrameWorkElement) para navegação usado pelo Frame interno.")]
        public Dictionary<string, FrameworkElement> Pages
        {
            get => (Dictionary<string, FrameworkElement>)GetValue(PagesProperty);
            set => SetValue(PagesProperty, value);
        }
        public static readonly DependencyProperty PagesProperty =
            DependencyProperty.Register(
                name: nameof(Pages),
                propertyType: typeof(Dictionary<string, FrameworkElement>),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: new Dictionary<string, FrameworkElement>()));

        [Category(Comum)]
        [Description("Obtem ou define um FrameworkElement que representa a página de fundo da aplicação, que será exibida se nenhum item estiver selecionado ou se a propriedade Pages não contiver nenhuma entrada.")]
        public FrameworkElement BackgroundPage
        {
            get => (FrameworkElement)GetValue(BackgroundPageProperty);
            set => SetValue(BackgroundPageProperty, value);
        }
        public static readonly DependencyProperty BackgroundPageProperty =
            DependencyProperty.Register(
                name: nameof(BackgroundPage),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(defaultValue: null));

        [Category(Comum)]
        [Description("Obtem ou define um FrameworkElement que será exibido caso a página solicitada não tenha nenhuma correspondência em Pages[].")]
        public FrameworkElement PageNotFoudedMessage
        {
            get => (FrameworkElement)GetValue(PageNotFoudedMessageProperty);
            set => SetValue(PageNotFoudedMessageProperty, value);
        }
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

        [Category(Comum)]
        [Description("Obtem ou define um valor que representa a visibilidade da barra de navegação do frame.")]
        public NavigationUIVisibility NavigationUIVisibility
        {
            get => (NavigationUIVisibility)GetValue(NavigationUIVisibilityProperty);
            set => SetValue(NavigationUIVisibilityProperty, value);
        }
        public static readonly DependencyProperty NavigationUIVisibilityProperty =
            DependencyProperty.Register(
                name: nameof(NavigationUIVisibility),
                propertyType: typeof(NavigationUIVisibility),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(NavigationUIVisibility.Hidden));

        [Category(Comum)]
        [Description("Obtem ou define um FrameworkElement personalizado localizado na parte inferior esquerda do controle.")]
        public FrameworkElement CustomArea
        {
            get => (FrameworkElement)GetValue(CustomAreaProperty);
            set => SetValue(CustomAreaProperty, value);
        }
        public static readonly DependencyProperty CustomAreaProperty =
            DependencyProperty.Register(
                name: nameof(CustomArea),
                propertyType: typeof(FrameworkElement),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: null));

        [Category(Comum)]
        [Description("Obtem ou define um valor que representa a parte sempre visível do painel esquerdo.")]
        public double MenuMinWidth
        {
            get => (double)GetValue(MenuMinWidthProperty);
            set => SetValue(MenuMinWidthProperty, value);
        }
        public static readonly DependencyProperty MenuMinWidthProperty =
            DependencyProperty.Register(
                name: nameof(MenuMinWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 55.0));

        [Category(Comum)]
        [Description("Obtem ou define um valor que representa a largura máxima do painel esquerdo.")]
        public double MenuMaxWidth
        {
            get => (double)GetValue(MenuMaxWidthProperty);
            set => SetValue(MenuMaxWidthProperty, value);
        }
        public static readonly DependencyProperty MenuMaxWidthProperty =
            DependencyProperty.Register(
                name: nameof(MenuMaxWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 320.0));

        [Category(Comum)]
        [Description("Obtem ou define um valor que representa a largura do painel esquerdo.")]
        public double MenuWidth
        {
            get => (double)GetValue(MenuWidthProperty);
            set => SetValue(MenuWidthProperty, value);
        }
        public static readonly DependencyProperty MenuWidthProperty =
            DependencyProperty.Register(
                name: nameof(MenuWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 320.0));

        [Description("Obtem ou define um pincel que descreve a cor do primeiro plano do botão que abre/fecha o painel esquerdo.")]
        public Brush HamburgerMenuForeground
        {
            get => (Brush)GetValue(HamburgerMenuForegroundProperty);
            set => SetValue(HamburgerMenuForegroundProperty, value);
        }
        public static readonly DependencyProperty HamburgerMenuForegroundProperty =
            DependencyProperty.Register(
                name: nameof(HamburgerMenuForeground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.GreenYellow));

        [Description("Obtem ou define um pincel que descreve a cor do GridSplitter.")]
        public Brush GridSplitterBackground
        {
            get => (Brush)GetValue(GridSplitterBackgroundProperty);
            set => SetValue(GridSplitterBackgroundProperty, value);
        }
        public static readonly DependencyProperty GridSplitterBackgroundProperty =
            DependencyProperty.Register(
                name: nameof(GridSplitterBackground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Brushes.Transparent));

        [Category(Comum)]
        [Description("Obtem ou define um valor que representa a espessura da GridSplitter.")]
        public double GridSplitterWidth
        {
            get => (double)GetValue(GridSplitterWidthProperty);
            set => SetValue(GridSplitterWidthProperty, value);
        }
        public static readonly DependencyProperty GridSplitterWidthProperty =
            DependencyProperty.Register(
                name: nameof(GridSplitterWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenu),
                typeMetadata: new PropertyMetadata(
                    defaultValue: 1.0));

        [Category(Comum)]
        [Description("Obtem ou define um valor que representa se o GridSplitter está habilitado.")]
        public bool GridSplitterIsEnabled
        {
            get => (bool)GetValue(GridSplitterIsEnabledProperty);
            set => SetValue(GridSplitterIsEnabledProperty, value);
        }
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PartFrame = GetTemplateChild<Frame>(PART_Frame);
            PartLeftPanelColumn = GetTemplateChild<ColumnDefinition>(PART_LeftPanelColumn);
            PartGridSplitter = GetTemplateChild<GridSplitter>(PART_GridSplitter);
            PartGridSplitter.DragCompleted -= GridSplitter_DragCompleted;
            PartGridSplitter.DragCompleted += GridSplitter_DragCompleted;
        }

        protected override DependencyObject GetContainerForItemOverride() => new SideMenuItem();
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