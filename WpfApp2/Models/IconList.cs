using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp2.Models
{
    public class IconList
    {
        private List<Icon> icons;
        public IconList()
        {
            icons = [];
            Load();
        }

        private void Load()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.TopDirectoryOnly);
                icons.Clear();
                foreach (var file in files)
                {
                    string name = Path.GetFileName(file);
                    icons.Add(new Icon(name, file));
                }
            }

        }

        public IReadOnlyList<Icon> GetIcons() => icons;
    }
}
