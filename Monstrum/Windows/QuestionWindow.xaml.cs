using System.Windows;
using System.Windows.Input;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public QuestionWindow()
        {
            InitializeComponent();
        }

        private void btYes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
        }

        private void btNo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }
    }
}
