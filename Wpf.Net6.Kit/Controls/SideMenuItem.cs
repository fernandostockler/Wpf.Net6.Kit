using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wpf.Net6.Kit.Controls
{
    /// <summary>
    /// Represents an item from the Items menu of the Side Menu control. 
    /// Side MenuItem derives from ListBox Item and adds some properties to adjust the icon, selected indicator and others.
    /// </summary>
    [TemplatePart(Name = PART_Rectangle, Type = typeof(Rectangle))]
    [TemplatePart(Name = PART_Symbol, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_Border, Type = typeof(Border))]
    public class SideMenuItem : ListBoxItem
    {
        private const string PART_Rectangle = "PART_Rectangle";
        private const string PART_Symbol = "PART_Symbol";
        private const string PART_Border = "PART_Border";
        private const string ClassName = "SideMenuItem";
        private const string SegoeMDL2Assets = "Segoe MDL2 Assets";
        private const string HomeSymbol = "\uE10F";

        static SideMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SideMenuItem), new FrameworkPropertyMetadata(typeof(SideMenuItem)));
        }

        /// <summary>
        /// Gets or sets the PageTypeName property that represents the key in the dictionary of the <see cref="SideMenu.Pages"/> property responsible for navigation.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets the PageTypeName property that represents the key in the dictionary of the Pages property of the associated SideMenu control responsible for navigation.")]
        public string PageTypeName
        {
            get => (string)GetValue(PageTypeNameProperty);
            set => SetValue(PageTypeNameProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="PageTypeName"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="PageTypeName"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty PageTypeNameProperty =
            DependencyProperty.Register(
                name: nameof(PageTypeName),
                propertyType: typeof(string),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: string.Empty));

        /// <summary>
        /// Gets or sets the brush representing the selection indicator color.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets the brush representing the selection indicator color.")]
        public Brush SelectionIndicatorBrush
        {
            get => (Brush)GetValue(SelectionIndicatorBrushProperty);
            set => SetValue(SelectionIndicatorBrushProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectionIndicatorBrush"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SelectionIndicatorBrush"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SelectionIndicatorBrushProperty =
            DependencyProperty.Register(
                name: nameof(SelectionIndicatorBrush),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new SolidColorBrush(Colors.YellowGreen)));

        /// <summary>
        /// Gets or sets the width of the selection indicator.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets the width of the selection indicator.")]
        public double SelectionIndicatorWidth
        {
            get => (double)GetValue(SelectionIndicatorWidthProperty);
            set => SetValue(SelectionIndicatorWidthProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectionIndicatorWidth"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SelectionIndicatorWidth"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SelectionIndicatorWidthProperty =
            DependencyProperty.Register(
                name: nameof(SelectionIndicatorWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: 5.0));

        /// <summary>
        /// Gets or sets the margin around the selection indicator.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets the margin around the selection indicator.")]
        public Thickness SelectionIndicatorMargin
        {
            get => (Thickness)GetValue(SelectionIndicatorMarginProperty);
            set => SetValue(SelectionIndicatorMarginProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SelectionIndicatorMargin"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SelectionIndicatorMargin"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SelectionIndicatorMarginProperty =
            DependencyProperty.Register(
                name: nameof(SelectionIndicatorMargin),
                propertyType: typeof(Thickness),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new Thickness(0, 0, 5, 0)));

        /// <summary>
        /// Gets or sets a string representing the symbol code displayed as an icon.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets a string representing the symbol code displayed as an icon.")]
        public string Symbol
        {
            get => (string)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Symbol"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="Symbol"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register(
                name: nameof(Symbol),
                propertyType: typeof(string),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: HomeSymbol));

        /// <summary>
        /// Gets or sets the FontFamily used by the Symbol property.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets the FontFamily used by the Symbol property.")]
        public FontFamily SymbolFontFamily
        {
            get => (FontFamily)GetValue(SymbolFontFamilyProperty);
            set => SetValue(SymbolFontFamilyProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SymbolFontFamily"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SymbolFontFamily"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SymbolFontFamilyProperty =
            DependencyProperty.Register(
                name: nameof(SymbolFontFamily),
                propertyType: typeof(FontFamily),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new FontFamily(SegoeMDL2Assets)));

        /// <summary>
        /// Gets or sets the brush representing the icon's color.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets the brush representing the icon's color.")]
        public Brush SymbolForeground
        {
            get => (Brush)GetValue(SymbolForegroundProperty);
            set => SetValue(SymbolForegroundProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SymbolForeground"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SymbolForeground"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SymbolForegroundProperty =
            DependencyProperty.Register(
                name: nameof(SymbolForeground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new FrameworkPropertyMetadata(defaultValue: Brushes.Black));

        /// <summary>
        /// Gets or sets a value of type double that represents the font size used to display the icon.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets a value of type double that represents the font size used to display the icon.")]
        public double SymbolFontSize
        {
            get => (double)GetValue(SymbolFontSizeProperty);
            set => SetValue(SymbolFontSizeProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SymbolFontSize"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SymbolFontSize"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SymbolFontSizeProperty =
            DependencyProperty.Register(
                name: nameof(SymbolFontSize),
                propertyType: typeof(double),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: 26.0));

        /// <summary>
        /// Gets or sets a thickness type value that represents the margin around the icon.
        /// </summary>
        [Category(ClassName)]
        [Description("Gets or sets a thickness type value that represents the margin around the icon.")]

        public Thickness SymbolMargin
        {
            get => (Thickness)GetValue(SymbolMarginProperty);
            set => SetValue(SymbolMarginProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="SymbolMargin"/> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="SymbolMargin"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SymbolMarginProperty =
            DependencyProperty.Register(
                name: nameof(SymbolMargin),
                propertyType: typeof(Thickness),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new Thickness(0, 0, 12, 0)));
    }
}