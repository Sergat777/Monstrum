using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Monstrum.Classes
{
    static
        class MediaHelper
    {
        public static string ResourcesPath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";

        public static string SoundsPath = ResourcesPath + "Sounds\\";

        public static string FilesPath = ResourcesPath + "Files\\";
        private static string[] _plot;
        private static Dictionary<string, string> _speaches;
        private static Dictionary<string, string> _itemsDescription;
        private static Dictionary<string, string> _bossSpeaches;

        public static string ImagesPath = ResourcesPath + "Images\\";
        public static string AmunitionsPath = ImagesPath + "Amunitions\\";
        public static string BackgroundsPath = ImagesPath + "BG\\";
        public static string BossesPath = ImagesPath + "Bosses\\";
        public static string MonstersPath = ImagesPath + "Monsters\\";

        private static MediaPlayer _currentMusic = new MediaPlayer();


        public static void StartWork()
        {
            SetBackground("field");
            SetGameMusic("beginMusic");

            DownloadFiles();

            _currentMusic.MediaEnded += MusicFinish;
            _timerDarkScreen.Tick += ChangeDark;
            _typingTimer.Tick += Type;
        }

        private static void DownloadFiles()
        {
            _plot = new string[8];

            StreamReader reader = new StreamReader(FilesPath + "Story.txt");
            _plot = reader.ReadToEnd().Split('|');
            reader = new StreamReader(FilesPath + "Speaches.txt");
            string[] speachesText = reader.ReadToEnd().Split(new string[] {"\r\n|"}, StringSplitOptions.None);
            reader = new StreamReader(FilesPath + "Amunitions.txt");
            string[] descriptions = reader.ReadToEnd().Split(new string[] { "\r\n|" }, StringSplitOptions.None);
            reader = new StreamReader(FilesPath + "BossStory.txt");
            string[] bossesWords = reader.ReadToEnd().Split(new string[] { "\r\n|" }, StringSplitOptions.None);

            reader.Close();
            _speaches = new Dictionary<string, string>(1000);
            _itemsDescription = new Dictionary<string, string>();
            _bossSpeaches = new Dictionary<string, string>();

            DownloadDictionary(_speaches, speachesText);
            DownloadDictionary(_itemsDescription, descriptions);
            DownloadDictionary(_bossSpeaches, bossesWords);
        }

        public static void DownloadDictionary(Dictionary<string, string> dict, string[] dictContent)
        {
            foreach (string part in dictContent)
            {
                string[] newItem = part.Split('_');
                dict.Add(newItem[0], newItem[1]);
            }
        }

        public static void SetMonsterImage(Image image, string monsterName)
        {
            image.Source = new BitmapImage(new Uri(MonstersPath + monsterName + ".png"));
        }

        public static void SetBackground(string backroundName)
        {
            ControllerManager.AppWindow.Background = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(BackgroundsPath + backroundName + ".jpg"))
            };
        }

        private static DispatcherTimer _timerDarkScreen = new DispatcherTimer(){ Interval = TimeSpan.FromMilliseconds(31.25) };
        private static double _opacityDarkScreen;
        private static bool _opasityGrow;
        private static Page _nextPage;

        public static void GoNewScreen(Page nextPage, string soundName = null)
        {
            if (soundName != null)
                PlayAudio(soundName);

            _opacityDarkScreen = 0;
            _opasityGrow = true;
            _nextPage = nextPage;
            _currentMusic.Volume = 0.25;
            ControllerManager.DarkScreen.Visibility = Visibility.Visible;
            _timerDarkScreen.Start();
        }

        public static void BlockUnBlockDarkScreen()
        {
            if (ControllerManager.KeysAreEnable)
            {
                ControllerManager.DarkScreen.Opacity = 0;
                ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
            }
            else
            {
                ControllerManager.DarkScreen.Opacity = 0.8;
                ControllerManager.DarkScreen.Visibility = Visibility.Visible;
            }

            ControllerManager.KeysAreEnable = !ControllerManager.KeysAreEnable;
        }

        public static void ChangeDark(object sender, EventArgs e)
        {
            if (_opacityDarkScreen >= 1)
            {
                _opasityGrow = false;
                ControllerManager.MainAppFrame.Navigate(_nextPage);
            }

            if (_opasityGrow)
                _opacityDarkScreen += 0.0390625;
            else
                _opacityDarkScreen -= 0.0390625;

            ControllerManager.DarkScreen.Opacity = _opacityDarkScreen;

            if (_opacityDarkScreen == 0)
            {
                _currentMusic.Volume = 0.25;
                ControllerManager.DarkScreen.Visibility = Visibility.Collapsed;
                _timerDarkScreen.Stop();
            }
        }

        private static DispatcherTimer _typingTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.05) };
        private static string _words;
        private static TextBlock _txtBlock;
        private static int _letterIndex;

        public static void SetTypingAnimation(TextBlock textPlace, string typingText)
        {
            _letterIndex = 0;
            _txtBlock = textPlace;
            _words = typingText;
            _typingTimer.Start();
        }

        public static void StopTypingAnimation()
        {
            _typingTimer.Stop();
        }

        public static void Type(object sender, EventArgs e)
        {
            if (_letterIndex != _words.Length)
            {
                if (_words[_letterIndex] == '\\' && _words[_letterIndex + 1] == 'n')
                {
                    _txtBlock.Text += "\n";
                    _letterIndex++;
                }
                else
                    _txtBlock.Text += _words[_letterIndex];

                _letterIndex++;
            }
            else
                _typingTimer.Stop();
        }

        private static void MusicFinish(object sender, EventArgs e)
        {
            _currentMusic.Position = TimeSpan.Zero;
            _currentMusic.Play();
        }

        public static void PlayAudio(string soundName)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(SoundsPath + soundName + ".wav"));
            player.Play();
        }

        public static void SetGameMusic(string musicName)
        {
            _currentMusic.Stop();
            _currentMusic.Open(new Uri(SoundsPath + musicName + ".wav"));
            _currentMusic.Play();
        }

        public static string GetStory(byte chapter)
        {
            return _plot[chapter];
        }

        public static string GetSpeach(string speachName)
        {
            return _speaches[speachName];
        }

        public static string GetItemDescription(string itemName)
        {
            return _itemsDescription[itemName];
        }

        public static string GetBossSpeach(string bossName)
        {
            return _bossSpeaches[bossName];
        }
    }
}
