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
using System.Windows.Threading;
using Monstrum.Classes;
using Monstrum.Classes.GameClasses;

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for GeneralGamePlayPage.xaml
    /// </summary>
    public partial class GeneralGamePlayPage : Page
    {
        DispatcherTimer timerOfTurns = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public GeneralGamePlayPage()
        {
            InitializeComponent();

            timerOfTurns.Tick += PlayerTurn;

            barEnemies.Maximum = GameSetter.EnemiesCounter;
            txtEnemies.Text = GameSetter.KillCounter + "/" + GameSetter.EnemiesCounter;

            Monster hero = new Monster(GameSetter.HeroName, GameSetter.HeroHealth,
                                                GameSetter.HeroDamage, GameSetter.HeroArmor);
            GameSetter.Hero = new MonsterView(hero);
            gridHero.Children.Add(GameSetter.Hero);

            GameSetter.Enemy = new MonsterView(GameSetter.GenerateMonster());
            gridEnemy.Children.Add(GameSetter.Enemy);
        }

        private void btHit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameSetter.Hero.Attack(GameSetter.Enemy);
            timerOfTurns.Start();
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;

            if (GameSetter.Enemy.IsDead())
            {
                GameSetter.KillCounter++;
                barEnemies.Value = GameSetter.KillCounter;
                txtEnemies.Text = GameSetter.KillCounter + "/" + GameSetter.EnemiesCounter;
            }
            else
            {
                GameSetter.Enemy.Attack(GameSetter.Hero);
                if (GameSetter.Hero.IsDead())
                    (new Windows.LoseWindow()).ShowDialog();
            }
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

        private void PlayerTurn(object sender, EventArgs e)
        {
            if (GameSetter.Enemy.IsDead())
            {
                gridEnemy.Children.Clear();
                GameSetter.Enemy = new MonsterView(GameSetter.GenerateMonster());
                gridEnemy.Children.Add(GameSetter.Enemy);
            }
            timerOfTurns.Stop();
            BlockUnBlockFrame();
            pnlAction.Opacity = 1;
        }

        private void BlockUnBlockFrame()
        {
            ControllerManager.MainAppFrame.IsEnabled = !ControllerManager.MainAppFrame.IsEnabled;
        }
    }
}
