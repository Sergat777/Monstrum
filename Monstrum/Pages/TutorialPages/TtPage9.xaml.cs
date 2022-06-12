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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monstrum.Pages.TutorialPages
{
    /// <summary>
    /// Interaction logic for TtPage9.xaml
    /// </summary>
    public partial class TtPage9 : Page
    {
        public TtPage9()
        {
            InitializeComponent();
        }

        private void btNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.TutorialFrame.Navigate(new TtPage10());
        }

        private void btPrevious_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.TutorialFrame.Navigate(new TtPage8());
        }
    }
}
