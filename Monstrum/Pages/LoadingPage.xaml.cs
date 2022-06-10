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
        private string currentStory;

        public LoadingPage()
        {
            InitializeComponent();
            currentStory = Classes.MediaHelper.GetStory(Classes.GameSetter.Chapter);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Classes.MediaHelper.SetGameMusic("loadingMusic");
            Classes.MediaHelper.SetBackground("1");
            txtHeader.Text = "ЧАСТЬ" + Classes.GameSetter.Chapter;
            Classes.MediaHelper.SetTypingAnimation(txtStory, currentStory);
        }

        private void btStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.MediaHelper.GoNewScreen(new GeneralGamePlayPage(), "winSound");
        }

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Classes.MediaHelper.StopTypingAnimation();
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
