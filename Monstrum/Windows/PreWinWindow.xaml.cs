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
using System.Windows.Threading;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for PreWinWindow.xaml
    /// </summary>
    public partial class PreWinWindow : Window
    {
        private DispatcherTimer typingTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.05) };
        private int letterIndex = 0;
        private string currentBossSpeach;

        public PreWinWindow(string bossName)
        {
            InitializeComponent();
            Classes.MediaHelper.BlockUnBlockDarkScreen();
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
            Classes.MediaHelper.BlockUnBlockDarkScreen();
        }

        private void DockPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            typingTimer.Stop();
            Close();
            Classes.MediaHelper.BlockUnBlockDarkScreen();
        }
    }
}
