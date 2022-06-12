using Monstrum.Classes;
using System.Windows;
using System.Windows.Input;

namespace Monstrum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ControllerManager.AppWindow = this;
            ControllerManager.MainAppFrame = mainAppFrame;
            ControllerManager.DarkScreen = darkScreen;

            MediaHelper.StartWork();

            mainAppFrame.Navigate(new Pages.MainMenuPage());
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                new Windows.PauseWindow().ShowDialog();
            else if (e.Key == Key.Tab)
                if (GameSetter.HeroCurrentHealth > GameSetter.HeroMaxHealth * 0.5)
                    new Windows.InventoryWindow().ShowDialog();
                else
                    new Windows.StopInventoryWindow().ShowDialog();
        }
    }
}
