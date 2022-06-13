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
    /// Interaction logic for WinWindow.xaml
    /// </summary>
    public partial class WinWindow : Window
    {
        public WinWindow()
        {
            InitializeComponent();
            Classes.MediaHelper.PlayAudio("winSound");
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;
            txtKills.Text = Classes.GameSetter.KillsCounter.ToString();
            txtSpares.Text = (Classes.GameSetter.FightsCounter - Classes.GameSetter.KillsCounter).ToString();
            txtTotalDamage.Text = Classes.GameSetter.TotalDamage.ToString();
            txtCruels.Text = Math.Round(Classes.GameSetter.BloodIndex, 3).ToString();
        }

        private void btMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)new QuestionWindow().ShowDialog())
            {
                Classes.ControllerManager.DarkScreen.Opacity = 0;
                Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
                Classes.ControllerManager.MainAppFrame.Navigate(new Pages.MainMenuPage());
                Close();
            }
        }

        private void btRepeat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
            Classes.ControllerManager.MainAppFrame.Navigate(new Pages.GeneralGamePlayPage());
            Close();
        }

        private void btNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
            Close();
            if (Classes.GameSetter.Chapter == 2)
            {
                MessageBox.Show("Спасибо за прохождение DEMO версии проекта MONSTRUM,\n" +
                    "Уважительная просьба, обеспечьте обратную связь, если вам понравилось:)", "DEMO VERSION",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Classes.ControllerManager.MainAppFrame.Navigate(new Pages.MainMenuPage());
            }
            else
            {
                Classes.GameSetter.SetNextChapter();
                Classes.ControllerManager.MainAppFrame.Navigate(new Pages.LoadingPage());
            }
        }
    }
}
