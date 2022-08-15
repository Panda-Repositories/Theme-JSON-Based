using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using WpfAnimatedGif;

namespace Panda_Theme_JSON_Format
{
    /// <summary>
    /// Interaction logic for LoaderWindow.xaml
    /// </summary>
    public partial class LoaderWindow : Window
    {
        public LoaderWindow()
        {
            InitializeComponent();
            LoadJSON();
        }

        private void Loader_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        #region JSON Theme
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
            bitmap.UriSource = new Uri(JsonData.Loader.BackgroundImageUri, UriKind.Absolute);
            bitmap.EndInit();
            ThemeBorder.ImageSource = bitmap;

            string extn = JsonData.Loader.BackgroundImageUri;
            switch (extn)
            {
                case "gif":
                    GifMode(JsonData.Loader.BackgroundImageUri);
                    gifborder.Visibility = Visibility.Visible;
                    break;
                default:
                    GifMode(JsonData.Loader.BackgroundImageUri);
                    gifborder.Visibility = Visibility.Hidden;
                    break;
            }
            this.Title = JsonData.Loader.Name;
        }

        private void GifMode(string _fs_name)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(_fs_name);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(gifborder, image);
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
