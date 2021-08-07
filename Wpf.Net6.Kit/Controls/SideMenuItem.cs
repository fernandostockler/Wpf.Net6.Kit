using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf.Net6.Kit.Controls
{
    public class SideMenuItem : ListBoxItem
    {
        private const string SideMenuItemCategory = "SideMenuItem";
        static SideMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SideMenuItem), new FrameworkPropertyMetadata(typeof(SideMenuItem)));
        }

        [Category(SideMenuItemCategory)]
        [Description("Obtem ou define a propriedade PageTypeName que representa a chave no dicionário da propriedade Pages do controle SideMenu associado, responsável pela navegação.")]
        public string PageTypeName
        {
            get => (string)GetValue(PageTypeNameProperty);
            set => SetValue(PageTypeNameProperty, value);
        }
        public static readonly DependencyProperty PageTypeNameProperty =
            DependencyProperty.Register(
                name: nameof(PageTypeName),
                propertyType: typeof(string),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(string.Empty));

        [Category(SideMenuItemCategory)]
        [Description("Obtem ou define o pincel que representa a cor do indicador de seleção.")]
        public Brush SelectionIndicatorBrush
        {
            get => (Brush)GetValue(SelectionIndicatorBrushProperty);
            set => SetValue(SelectionIndicatorBrushProperty, value);
        }
        public static readonly DependencyProperty SelectionIndicatorBrushProperty =
            DependencyProperty.Register(
                name: nameof(SelectionIndicatorBrush),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(
                    defaultValue: new SolidColorBrush(Colors.GreenYellow)));

        [Category(SideMenuItemCategory)]
        [Description("")]

        public double SelectionIndicatorWidth
        {
            get => (double)GetValue(SelectionIndicatorWidthProperty);
            set => SetValue(SelectionIndicatorWidthProperty, value);
        }
        public static readonly DependencyProperty SelectionIndicatorWidthProperty =
            DependencyProperty.Register(
                name: nameof(SelectionIndicatorWidth),
                propertyType: typeof(double),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(5.0));

        [Category(SideMenuItemCategory)]
        [Description("Obtem ou define a propriedade Symbol em PART_SymbolIcon. Só sera visível com a propriedade IconMode = IconMode.Symbol")]
        public Symbol Symbol
        {
            get => (Symbol)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register(
                name: nameof(Symbol),
                propertyType: typeof(Symbol),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(
                    defaultValue: Symbol.Home));
    }
}
