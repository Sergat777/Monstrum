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
using Monstrum.Classes.GameClasses;

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for GeneralGamePlayPage.xaml
    /// </summary>
    public partial class GeneralGamePlayPage : Page
    {
        public GeneralGamePlayPage()
        {
            InitializeComponent();

            Monster firstMonster = new Monster(Classes.GameSetter.HeroName, (float)9.5, 10);
            MonsterView monster = new MonsterView(firstMonster);
            gridHero.Children.Add(monster);
        }

        private void btHit_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void btBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btSpare_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
