using System.IO;
using Microsoft.Win32;

namespace WpfApp3.Models
{
    internal static class DataLoader
    {
        private static readonly string _baseDataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json");
        private static string? _dataFilePath = null;

        private static bool IsBaseFileAvailable => File.Exists(_baseDataFilePath);
        internal static string? DataFilePath
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(_dataFilePath)) return _dataFilePath;
                if (IsBaseFileAvailable) return _baseDataFilePath;
                OpenFileDialog dlg = new()
                {
                    Title = "Выберите данные о противниках",
                    FileName = $"data",
                    DefaultExt = ".json",
                    Filter = "JSON|*.json",
                };
                if (dlg.ShowDialog() == true)
                {
                    _dataFilePath = dlg.FileName;
                    return _dataFilePath;
                }
                return null;
            }
        }
    }
}
