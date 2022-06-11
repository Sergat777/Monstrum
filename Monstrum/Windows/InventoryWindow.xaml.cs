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
using System.Windows.Shapes;
using Monstrum.Classes;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            InitializeComponent();
            MediaHelper.SetMonsterImage(imgHero, GameSetter.HeroName);
            barHealth.Maximum = GameSetter.HeroHealth;
            barHealth.Value = GameSetter.Hero.GetMonster().GetHealth();
            txtHealth.Text = GameSetter.Hero.GetMonster().GetHealth().ToString() + "/" + GameSetter.HeroHealth;
            txtDamage.Text = GameSetter.HeroDamage.ToString();
            txtArmor.Text = GameSetter.HeroArmor.ToString();
            ControllerManager.DarkScreen.Opacity = 0.8;
            ControllerManager.DarkScreen.Visibility = Visibility.Visible;
            List<Classes.GameClasses.EquipmentView> views = new List<Classes.GameClasses.EquipmentView>();
            views.Add(new Classes.GameClasses.EquipmentView(GameSetter.GenerateEquipment()));
            views.Add(new Classes.GameClasses.EquipmentView(GameSetter.GenerateEquipment()));
            views.Add(new Classes.GameClasses.EquipmentView(GameSetter.GenerateEquipment()));
            views.Add(new Classes.GameClasses.EquipmentView(GameSetter.GenerateEquipment()));
            views.Add(new Classes.GameClasses.EquipmentView(GameSetter.GenerateEquipment()));
            views.Add(new Classes.GameClasses.EquipmentView(GameSetter.GenerateEquipment()));
            lvInventory.ItemsSource = views;
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
            Classes.GameClasses.EquipmentView selectedItem = (sender as Grid).DataContext as Classes.GameClasses.EquipmentView;
            txtDescription.Text = selectedItem.GetEquipmentDescription();

            int stat = (int)selectedItem.GetEquipmentStat();
            if (stat == 1)
                txtStatInfluence.Text = "БРОНЯ +";
            if (stat == 2)
                txtStatInfluence.Text = "ЗДОРОВЬЕ +";
            if (stat == 3)
                txtStatInfluence.Text = "УРОН +";

            txtStatInfluence.Text += selectedItem.GetEquipmentStatValue();
        }
    }
}
