using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
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
        /*
         * Panda-Development Team 
         * No Matter how terrible the code it is, least we share our code to all of you. It's really up for you to improve this code.
         * 
         */
        public MainWindow()
        {
            InitializeComponent();
            LoadJSON();
        }

        private async void LoadJSON(string path = "path")
        {
            ThemeBorder.ImageSource = null; //Null the Image for some reason lmfao

            //Unnecessary things, Either you could remove it or nah... basically just download the Theme.json if its not exist.
            if (!File.Exists("Theme.json"))
            {
                File.WriteAllText("Theme.json", new WebClient().DownloadString("https://cdn.discordapp.com/attachments/1007125174048534599/1008299967980511304/Panda_Default.json"));
            }
            string newjson = path; //WTF is this part.. soo, basically on my own code, You can just click browse browser and find the right theme without copy the thing to Panda Directory. Very Unnecessary Code.
            if (newjson != "path")
            {
                File.WriteAllText("Theme.json", newjson);
            }
            await Task.Delay(100);
            // This Part are Important

            string jsonString = AppDomain.CurrentDomain.BaseDirectory + @"\Theme.json"; //Location of Theme.json ( Your Theme )
            var JsonData = JsonConvert.DeserializeObject<ThemeAsset.User>(File.ReadAllText(jsonString)); //Deserializing it 



            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(JsonData.Themes.Theme_URL, UriKind.Absolute); //Get the string from JSON ( Theme_URL )
            bitmap.EndInit();
            ThemeBorder.ImageSource = bitmap;


            string extn = JsonData.Themes.Theme_URL; //Duhh.. Read the Theme_URL on JSON

            //Don't asked me why tf i put delay on it. basically sometimes it cause unexcepted handling issue. brub.

            if (extn.Contains(".gif")) //If the theme is a GIF, it redirect your thing to GIF Mode. this how we handle it for a moment brub
            {
                await Task.Delay(1500);
                GifMode(JsonData.Themes.Theme_URL); 
                gifborder.Visibility = Visibility.Visible;
            }
            else
            {
                await Task.Delay(500);
                GifMode(JsonData.Themes.Theme_URL);
                gifborder.Visibility = Visibility.Hidden;
            }
            this.Title = JsonData.Themes.Loader_Name;
        }

        private void GifMode(string filename)
        {
            var image = new BitmapImage(); 
            image.BeginInit();
            image.UriSource = new Uri(filename);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(gifborder, image);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Load the JSON Scripts
            var OpenFile = new OpenFileDialog
            {
                Title = "Import Configurations File",
                Filter = "Theme / Setting Files (*.json)|*.json",
            };

            if (OpenFile.ShowDialog() == true)
            {
                string filepath = OpenFile.FileName;
                LoadJSON(File.ReadAllText(filepath)); //Load the LoadJSON with the specific json.
            }
        }
    }
}
