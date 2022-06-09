using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
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

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void btPlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.MediaHelper.PlayAudio("startSound");
        }

        private void btCreator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.MainAppFrame.Navigate(new CreatorPage());
        }

        private void btClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
