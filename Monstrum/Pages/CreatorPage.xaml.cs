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

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for CreatorPage.xaml
    /// </summary>
    public partial class CreatorPage : Page
    {
        public CreatorPage()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            Classes.ControllerManager.MainAppFrame.GoBack();
        }
    }
}
