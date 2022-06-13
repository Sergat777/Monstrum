using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Monstrum.Classes.GameClasses
{
    internal class MonsterView : DockPanel
    {
        private Monster _monster;
        private static ImageBrush _dialogBackgroundImage = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(MediaHelper.BackgroundsPath + "wordBG.png"))
        };
        private static ImageBrush _hitBackgroundImage = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(MediaHelper.BackgroundsPath + "hitBG.png"))
        };
        private static ImageBrush _regenerationBackgroundImage = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(MediaHelper.BackgroundsPath + "regenerationBG.png"))
        };
        private DockPanel _healthPanel = new DockPanel()
        {
            Background = new SolidColorBrush(Color.FromRgb(245, 197, 154)),
            Margin = new Thickness(5)
        };
        private ProgressBar _healthBar = new ProgressBar();
        private TextBlock _healthCounter = new TextBlock()
        {
            FontSize = 32,
            VerticalAlignment = VerticalAlignment.Center
        };
        private StackPanel _armorInf = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(5)
        };
        private Image _shieldImg = new Image()
        {
            Source = new BitmapImage(new Uri(MediaHelper.AmunitionsPath + "shield3.png")),
            Margin = new Thickness(3),
            Height = 30
        };
        private TextBlock _armorCounter = new TextBlock()
        {
            FontSize = 32,
            VerticalAlignment = VerticalAlignment.Center
        };
        private Grid _imagePanel = new Grid()
        {
            Margin = new Thickness(30)
        };
        private Image _monsterImage = new Image()
        {
            Margin = new Thickness(5)
        };
        private Grid _damageBlock = new Grid()
        {
            Visibility = Visibility.Collapsed,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            Height = 150,
            Width = 150,
            Margin = new Thickness(5),
            Background = _hitBackgroundImage
        };
        private TextBlock _hitBlock = new TextBlock()
        {
            FontSize = 20,
            Foreground = Brushes.Black,
            FontWeight = FontWeights.Black,
            VerticalAlignment = VerticalAlignment.Center
        };
        private Grid _dialogBlock = new Grid()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            Height = 170,
            Width = 210,
            Background = _dialogBackgroundImage,
            Visibility = Visibility.Collapsed
        };
        private TextBlock _speachBlock = new TextBlock()
        {
            FontSize = 20,
            Foreground = Brushes.Black,
            Margin = new Thickness(10),
            VerticalAlignment = VerticalAlignment.Center
        };
        private Image _blockImage = new Image()
        {
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Height = 170,
            Source = new BitmapImage(new Uri(MediaHelper.BackgroundsPath + "shieldBG.png")),
            Visibility = Visibility.Collapsed
        };
        private Grid _regenBlock = new Grid()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Height = 170,
            Width = 210,
            Background = _regenerationBackgroundImage,
            Visibility = Visibility.Collapsed
        };
        private TextBlock _regenHealthBlock = new TextBlock()
        {
            FontSize = 20,
            Foreground = Brushes.Black,
            Margin = new Thickness(10),
            VerticalAlignment = VerticalAlignment.Center,
            FontWeight = FontWeights.Bold
        };
        private DispatcherTimer timerTalk = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };
        private DispatcherTimer timerHit = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.65) };
        private DispatcherTimer timerBlock = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.65) };
        private DispatcherTimer timerRegen = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.65) };

        public MonsterView(Monster monster)
        {
            // set properties of Main View
            _monster = monster;

            timerTalk.Tick += ShutUp;
            timerHit.Tick += HideHit;
            timerBlock.Tick += HideBlock;
            timerRegen.Tick += HideRegen;

            // Update controls
            UpdateHealthPanel();
            UpdateImage();


            // add health panel
            _armorInf.Children.Add(_shieldImg);
            _armorInf.Children.Add(_armorCounter);
            _armorInf.SetValue(DockProperty, Dock.Right);
            _healthPanel.SetValue(DockProperty, Dock.Top);
            _healthPanel.Children.Add(_armorInf);
            _healthCounter.SetValue(DockProperty, Dock.Right);
            _healthBar.SetValue(DockProperty, Dock.Left);
            _healthPanel.Children.Add(_healthCounter);
            _healthPanel.Children.Add(_healthBar);
            Children.Add(_healthPanel);

            // add image panel
            _regenBlock.Children.Add(_regenHealthBlock);
            _dialogBlock.Children.Add(_speachBlock);
            _damageBlock.Children.Add(_hitBlock);
            _imagePanel.Children.Add(_monsterImage);
            _imagePanel.Children.Add(_regenBlock);
            _imagePanel.Children.Add(_dialogBlock);
            _imagePanel.Children.Add(_damageBlock);
            _imagePanel.Children.Add(_blockImage);
            _imagePanel.SetValue(DockProperty, Dock.Top);
            Children.Add(_imagePanel);
        }

        public Monster GetMonster()
        {
            return _monster;
        }

        public bool TryRun(float enemyDamage)
        {
            _monster.TryRun(enemyDamage);
            if (_monster.GetIsEscaped())
                UpdateImage();
            else
                Speak();
            return _monster.GetIsEscaped();
        }

        public void Regeneration()
        {
            float regeneratedHealth = _monster.Regeneration();

            if (regeneratedHealth > 0)
                ShowRegen(regeneratedHealth);
        }

        public void Speak()
        {
            _speachBlock.Text = "";
            _dialogBlock.Visibility = Visibility.Visible;
            MediaHelper.SetTypingAnimation(_speachBlock, _monster.GenerateSpeach());
            timerTalk.Interval = TimeSpan.FromSeconds(3);
            timerTalk.Start();
        }

        public void ShutUp(object sender, EventArgs e)
        {
            _dialogBlock.Visibility = Visibility.Collapsed;
            _speachBlock.Text = null;
            timerTalk.Stop();
        }

        public void ShowHit(float damage)
        {
            _damageBlock.Visibility = Visibility.Visible;
            _hitBlock.Text = Math.Round(damage, 1).ToString();
            timerHit.Start();
        }

        public void HideHit(object sender, EventArgs e)
        {
            _damageBlock.Visibility = Visibility.Collapsed;
            _hitBlock.Text = null;
            timerHit.Stop();
        }

        public void ShowBlock()
        {
            _blockImage.Visibility = Visibility.Visible;
            MediaHelper.PlayAudio("shieldSound");
            timerBlock.Start();
        }

        public void HideBlock(object sender, EventArgs e)
        {
            _blockImage.Visibility = Visibility.Collapsed;
            timerBlock.Stop();
        }

        public void ShowRegen(float addedHealth)
        {
            _regenBlock.Visibility = Visibility.Visible;
            _regenHealthBlock.Text = Math.Round(addedHealth, 1).ToString();
            timerRegen.Start();
        }

        public void HideRegen(object sender, EventArgs e)
        {
            _regenBlock.Visibility = Visibility.Collapsed;
            timerRegen.Stop();
        }

        public void Block()
        {
            _monster.OnBlock();
        }

        public void Attack(MonsterView target)
        {
            target.ApplyDamage(_monster.GetDamage());
        }

        public void ApplyDamage(float damage)
        {
            bool wasBlock = _monster.GetIsBlock();
            _monster.ApplyDamage(damage);
            if (IsAttacked())
                if (wasBlock)
                    if (_monster.GetArmor() > 0)
                        ShowHit((float)Math.Round(damage - _monster.GetArmor() * 2, 1));
                    else
                        ShowHit(damage - 1);
                else
                    ShowHit(damage - _monster.GetArmor());
            else
                ShowBlock();

            UpdateHealthPanel();
            UpdateImage();
        }

        public bool IsAttacked()
        {
            return _monster.GetIsAttacked();
        }

        public bool IsEscaped()
        {
            return _monster.GetIsEscaped();
        }

        public bool IsDead()
        {
            return _healthBar.Value <= 0;
        }

        public void UpdateImage()
        {
            if (_monster is Boss)
                _monsterImage.Source = new BitmapImage(new Uri(MediaHelper.BossesPath + _monster.GetName() + ".png"));
            else
                MediaHelper.SetMonsterImage(_monsterImage, _monster.GetName());
        }

        public void UpdateHealthPanel()
        {
            _healthBar.Maximum = _monster.GetMaxHealth();
            _healthBar.Value = _monster.GetHealth();
            _healthCounter.Text = _monster.GetHealth().ToString() + "/" + _monster.GetMaxHealth();
            _armorCounter.Text = _monster.GetArmor().ToString();

            if (_monster.GetHealth() >= _monster.GetMaxHealth() * 0.35)
                _healthBar.Foreground = Brushes.LightGreen;
            else
                _healthBar.Foreground = Brushes.DarkRed;
        }
    }
}
