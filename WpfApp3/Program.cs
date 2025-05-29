using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    public static class Program
    {
        [STAThread] // WPF требует STAThread (Single-Threaded Apartment)
        public static void Main()
        {
            WpfApp3.Models.BigNumberV2 n = new("0");
            WpfApp3.Models.BigNumberV2 b = new("1");
            n = n - b;
            MessageBox.Show(n.ToString());

            Application app = new Application();
            app.Run(new MainWindow());
        }
    }
}
