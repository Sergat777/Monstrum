using System.Windows;
using System.Windows.Input;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for TutorialWindow.xaml
    /// </summary>
    public partial class TutorialWindow : Window
    {
        public TutorialWindow()
        {
            InitializeComponent();
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;
            tutorialFrame.Navigate(new Pages.TutorialPages.StartPage());
            Classes.ControllerManager.TutorialFrame = tutorialFrame;
            Classes.ControllerManager.TutorialWindow = this;
        }

        private void btSkip_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }

        private void btStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tutorialFrame.Navigate(new Pages.TutorialPages.TtPage1());
            gridButtons.Visibility = Visibility.Collapsed;
        }
    }
}
