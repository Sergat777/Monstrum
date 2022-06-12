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
    /// Interaction logic for TtPage4.xaml
    /// </summary>
    public partial class TtPage4 : Page
    {
        public TtPage4()
        {
            InitializeComponent();
        }

        private void btNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.TutorialFrame.Navigate(new TtPage5());
        }

        private void btPrevious_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.TutorialFrame.Navigate(new TtPage3());
        }
    }
}
