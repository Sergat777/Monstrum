using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monstrum.Classes.GameClasses
{
    internal class MonsterView : StackPanel
    {
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
        private Image _image = new Image()
        {
            Stretch = Stretch.Fill,
            Height = 300,
            Width = 300,
            Margin = new Thickness(5)
        };
        // private Grid _dialogGrid = new Grid();

        public MonsterView(Monster monster)
        {
            _monster = monster;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            UpdateHealthPanel();
            UpdateImage();

            _healthPanel.Children.Add(_healthBar);
            _healthPanel.Children.Add(_healthCounter);
            Children.Add(_healthPanel);
            Children.Add(_image);
            // Children.Add(_dialogGrid);
        }

        public Monster GetMonster()
        {
            return _monster;
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

        public void UpdateImage()
        {
            MediaHelper.SetMonsterImage(_image, _monster.GetName());
        }

        public bool IsDead()
        {
            if (_healthBar.Value <= 0)
                return true;
            else
                return false;
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
