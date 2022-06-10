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
    /// Interaction logic for LoseWindow.xaml
    /// </summary>
    public partial class LoseWindow : Window
    {
        public LoseWindow()
        {
            InitializeComponent();
            Classes.MediaHelper.PlayAudio("loseSound");
        }

        private void btMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            Classes.ControllerManager.MainAppFrame.Navigate(new Pages.MainMenuPage());
        }

        private void btExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
            App.Current.Shutdown();
        }

        private void btRepeat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            Classes.ControllerManager.MainAppFrame.Navigate(new Pages.GeneralGamePlayPage());
        }
    }
}
