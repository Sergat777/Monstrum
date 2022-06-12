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

namespace Monstrum.Pages.TutorialPages
{
    /// <summary>
    /// Interaction logic for TtPage8.xaml
    /// </summary>
    public partial class TtPage8 : Page
    {
        public TtPage8()
        {
            InitializeComponent();
        }

        private void btNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.TutorialFrame.Navigate(new TtPage9());
        }

        private void btPrevious_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ControllerManager.TutorialFrame.Navigate(new TtPage7());
        }
    }
}
