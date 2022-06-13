using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Monstrum.Classes;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        private Classes.GameClasses.EquipmentView _currentItem;

        public InventoryWindow()
        {
            InitializeComponent();

            ControllerManager.DarkScreen.Opacity = 0.8;
            ControllerManager.DarkScreen.Visibility = Visibility.Visible;

            MediaHelper.SetMonsterImage(imgHero, GameSetter.HeroName);
            lvInventory.ItemsSource = GameSetter.InventoryList;
            UpdateInventory();
        }

        private void btClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            ControllerManager.DarkScreen.Opacity = 0;
            ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
                Close();
            ControllerManager.DarkScreen.Opacity = 0;
            ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }

        private void item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btEquip.Visibility = Visibility.Visible;
            _currentItem = (sender as Grid).DataContext as Classes.GameClasses.EquipmentView;
            txtDescription.Text = _currentItem.GetEquipmentDescription();

            int stat = (int)_currentItem.GetEquipmentStat();
            if (stat == 1)
                txtStatInfluence.Text = "БРОНЯ +";
            if (stat == 2)
                txtStatInfluence.Text = "ЗДОРОВЬЕ +";
            if (stat == 3)
                txtStatInfluence.Text = "УРОН +";

            txtStatInfluence.Text += _currentItem.GetEquipmentStatValue();
        }

        private void UpdateInventory()
        {
            if (GameSetter.Weapon != null)
                imgWeapon.Source = new BitmapImage(new Uri(GameSetter.Weapon.ImageSource));
            if (GameSetter.Armor != null)
                imgArmor.Source = new BitmapImage(new Uri(GameSetter.Armor.ImageSource));
            if (GameSetter.Hat != null)
                imgHat.Source = new BitmapImage(new Uri(GameSetter.Hat.ImageSource));
            if (GameSetter.Shield != null)
                imgShield.Source = new BitmapImage(new Uri(GameSetter.Shield.ImageSource));

            barHealth.Maximum = GameSetter.HeroMaxHealth;
            barHealth.Value = GameSetter.HeroCurrentHealth;
            txtHealth.Text = Math.Round(GameSetter.HeroCurrentHealth, 1).ToString() + "/" + Math.Round(GameSetter.HeroMaxHealth, 1);
            txtDamage.Text = Math.Round(GameSetter.HeroDamage, 1).ToString();
            txtArmor.Text = Math.Round(GameSetter.HeroArmor, 1).ToString();
        }

        private void btEquip_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _currentItem.GetEquipment().SetIsEquip(true);
            btEquip.Visibility = Visibility.Hidden;

            int typeCurrentItem = (int)_currentItem.GetEquipmentType();

            Classes.GameClasses.EquipmentView previousItem = null;

            if (typeCurrentItem == 1 || typeCurrentItem == 4 || typeCurrentItem >= 6)
            {
                previousItem = GameSetter.Weapon;
                GameSetter.Weapon = _currentItem;
            }
            else if (typeCurrentItem == 2)
            {
                previousItem = GameSetter.Armor;
                GameSetter.Armor = _currentItem;
            }
            else if (typeCurrentItem == 3)
            {
                previousItem = GameSetter.Shield;
                GameSetter.Shield = _currentItem;
            }
            else if (typeCurrentItem == 5)
            {
                previousItem = GameSetter.Hat;
                GameSetter.Hat = _currentItem;
            }

            if (previousItem != null)
            {
                previousItem.GetEquipment().SetIsEquip(false);
                GameSetter.InventoryList.Add(previousItem);
                lvInventory.ItemsSource = GameSetter.InventoryList;
            }


            GameSetter.InventoryList.Remove(_currentItem);
            lvInventory.ItemsSource = null;
            lvInventory.ItemsSource = GameSetter.InventoryList;

            GameSetter.UpdateStats();
            GameSetter.Hero.UpdateHealthPanel();
            UpdateInventory();
        }

        private void imgHat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btEquip.Visibility = Visibility.Hidden;
            if (GameSetter.Hat != null)
            {
                txtDescription.Text = GameSetter.Hat.GetEquipmentDescription();

                int stat = (int)GameSetter.Hat.GetEquipmentStat();
                if (stat == 1)
                    txtStatInfluence.Text = "БРОНЯ +";
                if (stat == 2)
                    txtStatInfluence.Text = "ЗДОРОВЬЕ +";
                if (stat == 3)
                    txtStatInfluence.Text = "УРОН +";

                txtStatInfluence.Text += GameSetter.Hat.GetEquipmentStatValue();
            }
        }

        private void imgArmor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btEquip.Visibility = Visibility.Hidden;
            if (GameSetter.Armor != null)
            {
                txtDescription.Text = GameSetter.Armor.GetEquipmentDescription();

                int stat = (int)GameSetter.Armor.GetEquipmentStat();
                if (stat == 1)
                    txtStatInfluence.Text = "БРОНЯ +";
                if (stat == 2)
                    txtStatInfluence.Text = "ЗДОРОВЬЕ +";
                if (stat == 3)
                    txtStatInfluence.Text = "УРОН +";

                txtStatInfluence.Text += GameSetter.Armor.GetEquipmentStatValue();
            }
        }

        private void imgWeapon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btEquip.Visibility = Visibility.Hidden;
            if (GameSetter.Weapon != null)
            {
                txtDescription.Text = GameSetter.Weapon.GetEquipmentDescription();

                int stat = (int)GameSetter.Weapon.GetEquipmentStat();
                if (stat == 1)
                    txtStatInfluence.Text = "БРОНЯ +";
                if (stat == 2)
                    txtStatInfluence.Text = "ЗДОРОВЬЕ +";
                if (stat == 3)
                    txtStatInfluence.Text = "УРОН +";

                txtStatInfluence.Text += GameSetter.Weapon.GetEquipmentStatValue();
            }
        }

        private void imgShield_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btEquip.Visibility = Visibility.Hidden;
            if (GameSetter.Shield != null)
            {
                txtDescription.Text = GameSetter.Shield.GetEquipmentDescription();

                int stat = (int)GameSetter.Shield.GetEquipmentStat();
                if (stat == 1)
                    txtStatInfluence.Text = "БРОНЯ +";
                if (stat == 2)
                    txtStatInfluence.Text = "ЗДОРОВЬЕ +";
                if (stat == 3)
                    txtStatInfluence.Text = "УРОН +";

                txtStatInfluence.Text += GameSetter.Shield.GetEquipmentStatValue();
            }
        }
    }
}
