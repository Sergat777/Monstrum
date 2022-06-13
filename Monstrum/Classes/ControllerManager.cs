using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monstrum.Classes
{
    class ControllerManager
    {
        public static MainWindow AppWindow { get; set; }
        public static Window TutorialWindow { get; set; }
        public static Grid DarkScreen { get; set; }
        public static bool KeysAreEnable { get; set; } = false;
        public static Frame MainAppFrame { get; set; }
        public static Frame TutorialFrame { get; set; }
    }
}
