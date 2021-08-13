﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wpf.Net6.Kit.Controls
{
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

        [Category(ClassName)]
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
                typeMetadata: new PropertyMetadata(defaultValue: string.Empty));

        [Category(ClassName)]
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
                typeMetadata: new PropertyMetadata(defaultValue: new SolidColorBrush(Colors.YellowGreen)));

        [Category(ClassName)]
        [Description("Obtem ou define a largura do indicador de seleção.")]
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
                typeMetadata: new PropertyMetadata(defaultValue: 5.0));

        [Category(ClassName)]
        [Description("Obtem ou define a margim entorno do indicador de seleção.")]
        public Thickness SelectionIndicatorMargin
        {
            get => (Thickness)GetValue(SelectionIndicatorMarginProperty);
            set => SetValue(SelectionIndicatorMarginProperty, value);
        }
        public static readonly DependencyProperty SelectionIndicatorMarginProperty =
            DependencyProperty.Register(
                name: nameof(SelectionIndicatorMargin),
                propertyType: typeof(Thickness),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new Thickness(0, 0, 5, 0)));

        [Category(ClassName)]
        [Description("Obtem ou define uma cadeia de caracteres que representa o código do símbolo exibido como o icone.")]
        public string Symbol
        {
            get => (string)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register(
                name: nameof(Symbol),
                propertyType: typeof(string),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: HomeSymbol));

        [Category(ClassName)]
        [Description("Obtem ou define o FontFamily utilizado pela propridade Symbol.")]
        public FontFamily SymbolFontFamily
        {
            get => (FontFamily)GetValue(SymbolFontFamilyProperty);
            set => SetValue(SymbolFontFamilyProperty, value);
        }
        public static readonly DependencyProperty SymbolFontFamilyProperty =
            DependencyProperty.Register(
                name: nameof(SymbolFontFamily),
                propertyType: typeof(FontFamily),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new FontFamily(SegoeMDL2Assets)));

        [Category(ClassName)]
        [Description("Obtem ou define o pincel que representa a cor do icone.")]
        public Brush SymbolForeground
        {
            get => (Brush)GetValue(SymbolForegroundProperty);
            set => SetValue(SymbolForegroundProperty, value);
        }
        public static readonly DependencyProperty SymbolForegroundProperty =
            DependencyProperty.Register(
                name: nameof(SymbolForeground),
                propertyType: typeof(Brush),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new FrameworkPropertyMetadata(defaultValue: Brushes.Black));

        [Category(ClassName)]
        [Description("Obtem ou define um valor do tipo double que representa o tamanho da fonte utilizada para exibir o icone.")]
        public double SymbolFontSize
        {
            get => (double)GetValue(SymbolFontSizeProperty);
            set => SetValue(SymbolFontSizeProperty, value);
        }
        public static readonly DependencyProperty SymbolFontSizeProperty =
            DependencyProperty.Register(
                name: nameof(SymbolFontSize),
                propertyType: typeof(double),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: 26.0));

        [Category(ClassName)]
        [Description("Obtem ou define um valor do tipo Thickness que representa a margim entorno do icone.")]

        public Thickness SymbolMargin
        {
            get => (Thickness)GetValue(SymbolMarginProperty);
            set => SetValue(SymbolMarginProperty, value);
        }
        public static readonly DependencyProperty SymbolMarginProperty =
            DependencyProperty.Register(
                name: nameof(SymbolMargin),
                propertyType: typeof(Thickness),
                ownerType: typeof(SideMenuItem),
                typeMetadata: new PropertyMetadata(defaultValue: new Thickness(0, 0, 12, 0)));
    }
}