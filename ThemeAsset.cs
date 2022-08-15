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

        public class Theme_Configuration
        {
            /*
             * These URL must require ending with extension such as .jpeg or .gif
             * -----------------------------------------------------------------------------
             * 
             */
            public string Theme_URL { get; set; } //URL Them
            public string MainUI_Name { get; set; } //Name of UI
            public string Loader_Name { get; set; } //Loader Name UI ( if you have Loader )
            public string IconURL { get; set; } //Icon / Logo URL

        }

        public class User
        {
            public Theme_Configuration Themes;
        }
    }
}
