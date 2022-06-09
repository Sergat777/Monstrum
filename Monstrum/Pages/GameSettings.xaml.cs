﻿using System;
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

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for GameSettings.xaml
    /// </summary>
    public partial class GameSettings : Page
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        private void pnlEasy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (new Windows.QuestionWindow().ShowDialog().Value)
            {
                Classes.GameSetter.DificultLevel = 1;
                Classes.MediaHelper.PlayAudio("winSound");
                Classes.ControllerManager.MainAppFrame.Navigate(new LoadingPage());
            }
        }

        private void pnlMedium_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (new Windows.QuestionWindow().ShowDialog().Value)
            {
                Classes.GameSetter.DificultLevel = 2;
                Classes.MediaHelper.PlayAudio("winSound");
                Classes.ControllerManager.MainAppFrame.Navigate(new LoadingPage());
            }
        }

        private void pnlHard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (new Windows.QuestionWindow().ShowDialog().Value)
            {
                Classes.GameSetter.DificultLevel = 3;
                Classes.MediaHelper.PlayAudio("winSound");
                Classes.ControllerManager.MainAppFrame.Navigate(new LoadingPage());
            }
        }
    }
}
