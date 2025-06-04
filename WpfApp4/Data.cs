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

        public static List<Answer> Answers { get; } = Load<Answer>();
        public static List<Question> Questions { get; } = Load<Question>();
        public static List<Quiz> Quizzes { get; } = Load<Quiz>();

        private static List<T> Load<T>()
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
                return JsonSerializer.Deserialize<List<T>>(text) ?? [];
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
                List<T> list = typeof(T) switch
                {
                    Type t when t == typeof(Answer) => (List<T>)(object)Answers,
                    Type t when t == typeof(Question) => (List<T>)(object)Questions,
                    Type t when t == typeof(Quiz) => (List<T>)(object)Quizzes,
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
