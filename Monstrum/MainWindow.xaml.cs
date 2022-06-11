﻿using Monstrum.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Monstrum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ControllerManager.AppWindow = this;
            ControllerManager.MainAppFrame = mainAppFrame;
            ControllerManager.DarkScreen = darkScreen;

            MediaHelper.StartWork();

            GameSetter.GenerateEquipment();

            mainAppFrame.Navigate(new Pages.GeneralGamePlayPage());
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                new Windows.PauseWindow().ShowDialog();
            else if (e.Key == Key.Tab)
                new Windows.InventoryWindow().ShowDialog();
        }
    }
}
