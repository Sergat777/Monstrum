using System.Windows;
using System.Windows.Input;

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
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;
        }

        private void btMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
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
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
            Classes.ControllerManager.MainAppFrame.Navigate(new Pages.GeneralGamePlayPage());
        }
    }
}
