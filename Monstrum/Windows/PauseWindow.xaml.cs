using System.Windows;
using System.Windows.Input;

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
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
            Close();
        }

        private void btRepeat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)new QuestionWindow().ShowDialog())
            {
                Close();
                Classes.ControllerManager.DarkScreen.Opacity = 0;
                Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
                Classes.ControllerManager.MainAppFrame.Navigate(new Pages.GeneralGamePlayPage());
            }
        }

        private void btExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)new QuestionWindow().ShowDialog())
            {
                App.Current.Shutdown();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Classes.ControllerManager.DarkScreen.Opacity = 0;
                Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
                Close();
            }
        }
    }
}
