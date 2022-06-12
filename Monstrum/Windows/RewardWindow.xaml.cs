using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Monstrum.Windows
{
    /// <summary>
    /// Interaction logic for RewardWindow.xaml
    /// </summary>
    public partial class RewardWindow : Window
    {
        public RewardWindow()
        {
            InitializeComponent();
            Classes.ControllerManager.DarkScreen.Opacity = 0.8;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Visible;

            Classes.GameClasses.EquipmentView newItem = new Classes.GameClasses.EquipmentView(Classes.GameSetter.GenerateEquipment());
            Classes.GameSetter.InventoryList.Add(newItem);
            imgNewItem.Source = new BitmapImage(new Uri(newItem.ImageSource));
        }

        private void btOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            Classes.ControllerManager.DarkScreen.Opacity = 0;
            Classes.ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
        }
    }
}
