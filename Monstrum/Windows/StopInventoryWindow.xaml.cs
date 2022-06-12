using System.Windows;
using System.Windows.Input;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for StopInventoryWindow.xaml
    /// </summary>
    public partial class StopInventoryWindow : Window
    {
        public StopInventoryWindow()
        {
            InitializeComponent();
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;
        }

        private void btOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }
    }
}
