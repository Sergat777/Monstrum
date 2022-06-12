using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        private DispatcherTimer typingTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.075) };
        private int letterIndex = 0;
        private string currentStory;

        public LoadingPage()
        {
            InitializeComponent();
            currentStory = Classes.MediaHelper.GetStory(Classes.GameSetter.Chapter);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Classes.MediaHelper.SetGameMusic("loadingMusic");
            Classes.MediaHelper.SetBackground(Classes.GameSetter.Chapter.ToString());
            txtHeader.Text = "ЧАСТЬ" + Classes.GameSetter.Chapter;
            typingTimer.Tick += Type;
            typingTimer.Start();
        }

        public void Type(object sender, EventArgs e)
        {
            if (Classes.ControllerManager.DarkScreen.Visibility == Visibility.Collapsed)
            {
                if (letterIndex != currentStory.Length)
                {
                    if (currentStory[letterIndex] == '\\' && currentStory[letterIndex + 1] == 'n')
                    {
                        txtStory.Text += "\n";
                        letterIndex++;
                    }
                    else
                        txtStory.Text += currentStory[letterIndex];

                    letterIndex++;
                }
                else
                {
                    typingTimer.Stop();
                    Thread.Sleep(2000);
                    imgLoad.Visibility = Visibility.Collapsed;
                    btStart.Visibility = Visibility.Visible;
                }
            }
        }

        private void btStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.MediaHelper.GoNewScreen(new GeneralGamePlayPage(), "winSound");
        }

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            typingTimer.Stop();
            txtStory.Text = "";
            for (int i = 0; i < currentStory.Length; i++)
            {
                if (currentStory[i] == '\\' && currentStory[i + 1] == 'n')
                {
                    txtStory.Text += "\n";
                    i++;
                }
                else
                    txtStory.Text += currentStory[i];
            }
            imgLoad.Visibility = Visibility.Collapsed;
            btStart.Visibility = Visibility.Visible;
        }
    }
}
