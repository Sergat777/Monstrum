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
        DispatcherTimer timerOfTurns = new DispatcherTimer();

        public GeneralGamePlayPage()
        {
            InitializeComponent();

            timerOfTurns.Tick += PlayerTurn;

            GameSetter.SetStandart();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MediaHelper.SetGameMusic("standartMusic");

            barEnemies.Maximum = GameSetter.EnemiesCounter;
            txtEnemies.Text = GameSetter.FightsCounter + "/" + GameSetter.EnemiesCounter;

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
            MediaHelper.PlayAudio("splashSound");
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;
            timerOfTurns.Interval = TimeSpan.FromSeconds(3.5);
            if (!GameSetter.Enemy.IsDead())
            {
                timerOfTurns.Interval = TimeSpan.FromSeconds(1.25);
                GameSetter.Enemy.Attack(GameSetter.Hero);
                MediaHelper.PlayAudio("damageSound");
                if (GameSetter.Hero.IsDead())
                    (new Windows.LoseWindow()).ShowDialog();
            }
            else
                timerOfTurns.Interval = TimeSpan.FromSeconds(3.5);

            GameSetter.Enemy.Speak();
            timerOfTurns.Start();
        }

        private void btBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameSetter.Hero.Block();
            MediaHelper.PlayAudio("shieldSound");
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;
            timerOfTurns.Interval = TimeSpan.FromSeconds(1.25);
            timerOfTurns.Start();
            if (!GameSetter.Enemy.IsDead())
            {
                GameSetter.Enemy.Attack(GameSetter.Hero);
                MediaHelper.PlayAudio("damageSound");
                if (GameSetter.Hero.IsDead())
                    (new Windows.LoseWindow()).ShowDialog();
            }
        }

        private void btSpare_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;
            if (!GameSetter.Enemy.TryRun(GameSetter.HeroDamage))
            {
                timerOfTurns.Interval = TimeSpan.FromSeconds(1);
                GameSetter.Enemy.Attack(GameSetter.Hero);
                MediaHelper.PlayAudio("damageSound");
                if (GameSetter.Hero.IsDead())
                    (new Windows.LoseWindow()).ShowDialog();
            }
            else
                timerOfTurns.Interval = TimeSpan.FromSeconds(3.5);

            GameSetter.Enemy.Speak();
            timerOfTurns.Start();
        }

        private void btInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Windows.InventoryWindow().ShowDialog();
        }

        private void PlayerTurn(object sender, EventArgs e)
        {
            if (GameSetter.Enemy.IsDead() || GameSetter.Enemy.IsEscaped())
            {
                if (GameSetter.Enemy.IsDead())
                    GameSetter.KillsCounter++;

                MediaHelper.PlayAudio("killSound");
                GameSetter.IncreaseFightsCounter();
                barEnemies.Value = GameSetter.FightsCounter;
                txtEnemies.Text = GameSetter.FightsCounter + "/" + GameSetter.EnemiesCounter;
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
