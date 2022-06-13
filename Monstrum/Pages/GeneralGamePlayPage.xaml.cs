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
        DispatcherTimer timerOfTurns = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1.25) };

        public GeneralGamePlayPage()
        {
            InitializeComponent();

            timerOfTurns.Tick += PlayerTurn;

            GameSetter.SetStandart();

            if (GameSetter.Chapter == 1)
                new Windows.TutorialWindow().ShowDialog();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MediaHelper.SetGameMusic("standartMusic");

            barEnemies.Maximum = GameSetter.EnemiesCounter;
            txtEnemies.Text = GameSetter.FightsCounter + "/" + GameSetter.EnemiesCounter;

            Monster hero = new Monster(GameSetter.HeroName, GameSetter.HeroMaxHealth,
                                                GameSetter.HeroDamage, GameSetter.HeroArmor);
            GameSetter.Hero = new MonsterView(hero);
            gridHero.Children.Add(GameSetter.Hero);
            GameSetter.UpdateStats();

            GameSetter.Enemy = new MonsterView(GameSetter.GenerateMonster());
            gridEnemy.Children.Add(GameSetter.Enemy);

            ControllerManager.KeysAreEnable = true;
        }

        private void btHit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameSetter.Hero.Regeneration();
            GameSetter.Hero.Attack(GameSetter.Enemy);
            GameSetter.UpdateStats();
            GameSetter.TotalDamage += GameSetter.HeroDamage - GameSetter.Enemy.GetMonster().GetArmor();
            MediaHelper.PlayAudio("splashSound");
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;

            if (!GameSetter.Enemy.IsDead())
                EnemyAttack();
            
            timerOfTurns.Interval = TimeSpan.FromSeconds(2);
            GameSetter.Enemy.Speak();
            timerOfTurns.Start();
        }

        private void btBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameSetter.Hero.Regeneration();
            GameSetter.UpdateStats();
            GameSetter.Hero.Block();
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;

            if (!GameSetter.Enemy.IsDead())
                EnemyAttack();

            timerOfTurns.Start();
        }

        private void btSpare_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameSetter.Hero.Regeneration();
            GameSetter.UpdateStats();
            BlockUnBlockFrame();
            pnlAction.Opacity = 0.5;
            if (!GameSetter.Enemy.TryRun(GameSetter.HeroDamage))
                EnemyAttack();
            else
                timerOfTurns.Interval = TimeSpan.FromSeconds(3.5);

            GameSetter.Enemy.Speak();
            timerOfTurns.Start();
        }

        private void btInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GameSetter.HeroCurrentHealth > GameSetter.HeroMaxHealth * 0.5)
                new Windows.InventoryWindow().ShowDialog();
            else
                new Windows.StopInventoryWindow().ShowDialog();
        }

        private void PlayerTurn(object sender, EventArgs e)
        {
            if (GameSetter.Enemy.IsDead() || GameSetter.Enemy.IsEscaped())
            {
                if (GameSetter.Enemy.IsDead())
                {
                    GameSetter.KillsCounter++;
                    GameSetter.BloodIndex += GameSetter.KillsCounter * 0.001F;
                }

                MediaHelper.PlayAudio("killSound");
                gridEnemy.Children.Clear();
                GameSetter.IncreaseFightsCounter();
                if (GameSetter.Boss != null)
                    GameSetter.Enemy = GameSetter.Boss;
                else
                    GameSetter.Enemy = new MonsterView(GameSetter.GenerateMonster());
                gridEnemy.Children.Add(GameSetter.Enemy);
                barEnemies.Value = GameSetter.FightsCounter;
                txtEnemies.Text = GameSetter.FightsCounter + "/" + GameSetter.EnemiesCounter;
            }

            timerOfTurns.Stop();
            BlockUnBlockFrame();
            pnlAction.Opacity = 1;
        }

        private void EnemyAttack()
        {
            timerOfTurns.Interval = TimeSpan.FromSeconds(1.25);
            GameSetter.Enemy.Attack(GameSetter.Hero);
            MediaHelper.PlayAudio("damageSound");
            if (GameSetter.Hero.IsDead())
                (new Windows.LoseWindow()).ShowDialog();
        }

        private void BlockUnBlockFrame()
        {
            ControllerManager.MainAppFrame.IsEnabled = !ControllerManager.MainAppFrame.IsEnabled;
        }

        private void btPause_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new Windows.PauseWindow().ShowDialog();
        }
    }
}
