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
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        public PauseWindow()
        {
            InitializeComponent();
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;
        }

        private void btContinue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
            Close();
        }

        private void btRepeat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)new QuestionWindow().ShowDialog())
            {
                DialogResult = true;
                Classes.ControllerManager.DarkScreen.Opacity = 0;
                Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
                Classes.ControllerManager.MainAppFrame.Navigate(new Pages.GeneralGamePlayPage());
            }
        }

        private void btExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)new QuestionWindow().ShowDialog())
            {
                DialogResult = false;
                App.Current.Shutdown();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Classes.ControllerManager.DarkScreen.Opacity = 0;
                Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
                Close();
            }
        }
    }
}
