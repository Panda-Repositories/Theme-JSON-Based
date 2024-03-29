﻿using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace Panda_Theme_JSON_Format
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadJSON();
        }
        private async void LoadJSON(string path = "path")
        {
            ThemeBorder.ImageSource = null;
            if (!File.Exists("Theme.json"))
            {
                Console.WriteLine("Creating Default JSON");
                string jsondefault = new WebClient().DownloadString("https://pastebin.com/raw/gK0bf25B");
                File.WriteAllText("Theme.json", jsondefault);
            }
            string newjson = path;
            if (newjson != "path")
            {
                File.WriteAllText("Theme.json", newjson);
            }
            await Task.Delay(100);

            string jsonString = AppDomain.CurrentDomain.BaseDirectory + @"\Theme.json";
            var JsonData = JsonConvert.DeserializeObject<ThemeAsset.User>(File.ReadAllText(jsonString)); 

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(JsonData.Main.BackgroundImageUri, UriKind.Absolute);
            bitmap.EndInit();
            ThemeBorder.ImageSource = bitmap;

            string extn = JsonData.Main.BackgroundImageUri;
            switch (extn)
            {
                case "gif":
                    GifMode(JsonData.Main.BackgroundImageUri);
                    gifborder.Visibility = Visibility.Visible;
                    break;
                default:
                    GifMode(JsonData.Main.BackgroundImageUri);
                    gifborder.Visibility = Visibility.Hidden;
                    break;
            }
            this.Title = JsonData.Main.Name;
        }

        private void GifMode(string _fs_name)
        {
            var image = new BitmapImage(); 
            image.BeginInit();
            image.UriSource = new Uri(_fs_name);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(gifborder, image);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var OpenFile = new OpenFileDialog
            {
                Title = "Import Configurations File",
                Filter = "Theme / Setting Files (*.json)|*.json",
            };
            using (var stream = OpenFile.OpenFile())
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        LoadJSON(reader.ReadToEnd());
                    }
                }
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Loader_Click(object sender, RoutedEventArgs e)
        {
            new LoaderWindow().Show();
        }
    }
}
