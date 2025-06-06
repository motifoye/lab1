using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WpfApp4.Models;

namespace WpfApp4
{
    internal static class Data
    {
        private static readonly string path = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true // форматирование JSON
        };

        public static ObservableCollection<Answer> Answers { get; } = Load<Answer>();
        public static ObservableCollection<Question> Questions { get; } = Load<Question>();
        public static ObservableCollection<Quiz> Quizzes { get; } = Load<Quiz>();

        static Data()
        {
            Answers.CollectionChanged += (s, e) => Save<Answer>();
            Questions.CollectionChanged += (s, e) => Save<Question>();
            Quizzes.CollectionChanged += (s, e) => Save<Quiz>();
        }

        private static ObservableCollection<T> Load<T>()
        {
            string filename = typeof(T).Name + ".json";
            string fullPath = Path.Combine(path, filename);
            string? text;

            try
            {
                text = File.ReadAllText(fullPath);
            }
            catch
            {
                File.Create(fullPath).Dispose();
                return [];
            }

            if (string.IsNullOrWhiteSpace(text))
                return [];

            try
            {   
                return JsonSerializer.Deserialize<ObservableCollection<T>>(text) ?? [];
            }
            catch
            {
                return [];
            }
        }

        public static bool Save<T>()
        {
            try
            {
                ObservableCollection<T> list = typeof(T) switch
                {
                    Type t when t == typeof(Answer) => (ObservableCollection<T>)(object)Answers,
                    Type t when t == typeof(Question) => (ObservableCollection<T>)(object)Questions,
                    Type t when t == typeof(Quiz) => (ObservableCollection<T>)(object)Quizzes,
                    _ => throw new NotSupportedException($"Saving type {typeof(T).Name} is not supported.")
                };

                string json = JsonSerializer.Serialize(list, JsonOptions);

                string fileName = typeof(T).Name + ".json";
                string fullPath = Path.Combine(path, fileName);

                Directory.CreateDirectory(path);
                File.WriteAllText(fullPath, json);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
