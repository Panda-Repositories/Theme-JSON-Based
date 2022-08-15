using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_Theme_JSON_Format
{
    public class ThemeAsset
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string GetPath
        {
            get { return path; }
        }

        public class MainConfiguration
        {
            public string BackgroundImageUri { get; set; }
            public string Name { get; set; }
        }

        public class LoaderConfiguration
        {
            public string BackgroundImageUri { get; set; }
            public string Name { get; set; }
        }

        public class User
        {
            public MainConfiguration Main;
            public LoaderConfiguration Loader;
        }
    }
}
