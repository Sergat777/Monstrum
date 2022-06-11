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
    internal class MonsterView : StackPanel
    {
        private Random rndm = new Random();
        private static ImageBrush dialogBackgroundImage = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(MediaHelper.BackgroundsPath + "wordBG.png"))
        };
        private Monster _monster;
        private StackPanel _healthPanel = new StackPanel()
        {
            Background = new SolidColorBrush(Color.FromRgb(245, 197, 154)),
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(5)
        };
        private ProgressBar _healthBar = new ProgressBar();
        private TextBlock _healthCounter = new TextBlock()
        {
            FontSize = 32,
            VerticalAlignment = VerticalAlignment.Center
        };
        private Grid _imagePanel = new Grid()
        {
            Height = 400,
            Width = 400
        };
        private Image _image = new Image()
        {
            Stretch = Stretch.Fill,
            Margin = new Thickness(5)
        };
        private Grid _dialogBlock = new Grid()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            Height = 170,
            Width = 200,
            Background = dialogBackgroundImage,
            Visibility = Visibility.Collapsed
        };
        private TextBlock _speachBlock = new TextBlock()
        {
            FontSize = 20,
            Foreground = Brushes.Black,
            VerticalAlignment = VerticalAlignment.Center
        };
        private DispatcherTimer timerTalk = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };

        public MonsterView(Monster monster)
        {
            // set properties of Main View
            _monster = monster;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            timerTalk.Tick += ShutUp;

            // Update controls
            UpdateHealthPanel();
            UpdateImage();

            // add health panel
            _healthPanel.Children.Add(_healthBar);
            _healthPanel.Children.Add(_healthCounter);
            Children.Add(_healthPanel);

            // add image panel
            _imagePanel.Children.Add(_image);
            _dialogBlock.Children.Add(_speachBlock);
            _imagePanel.Children.Add(_dialogBlock);

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

        public void Speak()
        {
            _speachBlock.Text = "";
            _dialogBlock.Visibility = Visibility.Visible;
            MediaHelper.SetTypingAnimation(_speachBlock, GenerateSpeach());
            timerTalk.Interval = TimeSpan.FromSeconds(3);
            timerTalk.Start();
        }

        public string GenerateSpeach()
        {
            string speach = "";

            if (IsDead())
            {
                speach += "bloodSpeach";
                speach += rndm.Next(1, 11);
            }
            else if (IsEscaped())
            {
                speach += "escapeSpeach";
                speach += rndm.Next(1, 6);
            }
            else
            {
                speach += "angrySpeach";
                speach += rndm.Next(1, 6);
            }

            speach = MediaHelper.GetSpeach(speach);

            return speach;
        }

        public void ShutUp(object sender, EventArgs e)
        {
            _dialogBlock.Visibility = Visibility.Collapsed;
            _speachBlock.Text = null;
            timerTalk.Stop();
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
            _monster.ApplyDamage(damage);
            UpdateHealthPanel();
            UpdateImage();
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
            MediaHelper.SetMonsterImage(_image, _monster.GetName());
        }

        public void UpdateHealthPanel()
        {
            _healthBar.Maximum = _monster.GetMaxHealth();
            _healthBar.Value = _monster.GetHealth();
            _healthCounter.Text = _monster.GetHealth().ToString() + "/" + _monster.GetMaxHealth();

            if (_monster.GetHealth() >= _monster.GetMaxHealth() * 0.35)
                _healthBar.Foreground = Brushes.LightGreen;
            else
                _healthBar.Foreground = Brushes.DarkRed;
        }
    }
}
