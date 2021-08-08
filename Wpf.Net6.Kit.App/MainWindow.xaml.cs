using System;
using System.Collections.Generic;
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
using Wpf.Net6.Kit.App.Views;
using Wpf.Net6.Kit.Controls;

namespace Wpf.Net6.Kit.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            sideMenu.Pages.Clear();
            sideMenu.Pages.Add(nameof(Page1), new Page1());
            sideMenu.Pages.Add(nameof(Page2), new Page2());
            sideMenu.Pages.Add(nameof(UserControl1), new UserControl1());
            sideMenu.Pages.Add(nameof(UserControl2), new UserControl2());
        }

        private void TryKioskModeButton_Click(object sender, RoutedEventArgs e)
        {
            KioskMode = true;
        }

        private void TryCustomDialogButton_Click(object sender, RoutedEventArgs e)
        {
            ShowCustomDialog = !ShowCustomDialog;
        }

        private void sideMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
