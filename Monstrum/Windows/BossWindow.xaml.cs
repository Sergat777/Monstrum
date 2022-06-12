using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for BossWindow.xaml
    /// </summary>
    public partial class BossWindow : Window
    {
        private DispatcherTimer typingTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.075) };
        private int letterIndex = 0;
        private string currentBossSpeach;

        public BossWindow(string bossName)
        {
            InitializeComponent();
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;
            imgBoss.Source = new BitmapImage(new Uri(Classes.MediaHelper.BossesPath + bossName + ".png"));
            currentBossSpeach = Classes.MediaHelper.GetBossSpeach(bossName);
            typingTimer.Tick += Type;
            typingTimer.Start();
        }

        public void Type(object sender, EventArgs e)
        {
            if (letterIndex != currentBossSpeach.Length)
            {
                if (currentBossSpeach[letterIndex] == '\\' && currentBossSpeach[letterIndex + 1] == 'n')
                {
                    txtBossStory.Text += "\n";
                    letterIndex++;
                }
                else
                    txtBossStory.Text += currentBossSpeach[letterIndex];

                letterIndex++;
            }
            else
            {
                typingTimer.Stop();
                Task.Delay(2000);
                btOk.Visibility = Visibility.Visible;
            }
        }

        private void btOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }

        private void DockPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            typingTimer.Stop();
            Close();
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }
    }
}
