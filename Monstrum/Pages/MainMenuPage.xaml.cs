﻿using System;
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

namespace Monstrum.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();

            imgMusic.Source = new BitmapImage(new Uri(Classes.MediaHelper.ImagesPath +
                "music_" + Convert.ToInt16(Classes.MediaHelper.IsPlayingMusic) + ".png"));
            imgSound.Source = new BitmapImage(new Uri(Classes.MediaHelper.ImagesPath +
                 "sound_" + Convert.ToInt16(Classes.MediaHelper.IsPlayingSound) + ".png"));
            Classes.MediaHelper.StartWork();
        }

        private void btPlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.MediaHelper.GoNewScreen(new GameSettings(), "startSound");
        }

        private void btCreator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.MainAppFrame.Navigate(new CreatorPage());
        }

        private void btClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (new Windows.QuestionWindow().ShowDialog().Value)
                Application.Current.Shutdown();
        }

        private void btMuteMusic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.MediaHelper.SetMusicMute();
            imgMusic.Source = new BitmapImage(new Uri(Classes.MediaHelper.ImagesPath +
                "music_" + Convert.ToInt16(Classes.MediaHelper.IsPlayingMusic) + ".png"));
        }

        private void btMuteSound_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.MediaHelper.IsPlayingSound = !Classes.MediaHelper.IsPlayingSound;
            imgSound.Source = new BitmapImage(new Uri(Classes.MediaHelper.ImagesPath +
                "sound_" + Convert.ToInt16(Classes.MediaHelper.IsPlayingSound) + ".png"));
        }
    }
}
