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
    }
}