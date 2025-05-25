using System.IO;
using System.Text.Json.Serialization;

namespace WpfApp2.Models
{
    public class Icon
    {
        [JsonInclude]
        private string name;
        [JsonInclude]
        private string fullPath;

        public Icon(string name, string path)
        {
            this.name = name;
            fullPath = path;
            //fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", name);
        }

        public string Name() => name;
        public string FullPath() => fullPath;
    }
}
