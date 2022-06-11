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
using System.Windows.Shapes;

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
            DialogResult = true;
            Close();
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }
    }
}
