using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Monstrum.Classes
{
    static
        class MediaHelper
    {
        public static string ResourcesPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string SoundsPath = ResourcesPath + "Sounds\\";
        public static string ImagesPath = ResourcesPath + "Images\\";
        public static string AmunitionsPath = ImagesPath + "Amunitions\\";
        public static string BackgroundsPath = ImagesPath + "BG\\";
        public static string BossesPath = ImagesPath + "Bosses\\";
        public static string MonstersPath = ImagesPath + "Monsters\\";

        private static SoundPlayer currentMusic = new SoundPlayer();

        public static void PlayAudio(string soundName)
        {
            (new SoundPlayer(SoundsPath + soundName + ".wav")).PlaySync();
            
           PlayMusic();
        }

        public static void SetBackground(string backroundName)
        {
            ControllerManager.AppWindow.Background = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(BackgroundsPath + backroundName + ".jpg"))
            };
        }

        public static void SetGameMusic(string musicName)
        {
            currentMusic.Stop();
            currentMusic.SoundLocation = SoundsPath + musicName + ".wav";
            PlayMusic();

        }

        public static void PlayMusic()
        {
            currentMusic.PlayLooping();
        }
    }
}
