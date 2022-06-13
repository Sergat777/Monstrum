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
            tutorialFrame.Navigate(new Pages.TutorialPages.StartPage());
            Classes.ControllerManager.TutorialFrame = tutorialFrame;
            Classes.ControllerManager.TutorialWindow = this;
        }

        private void btSkip_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tutorialFrame.Navigate(new Pages.TutorialPages.TtPage1());
            gridButtons.Visibility = Visibility.Collapsed;
        }
    }
}
