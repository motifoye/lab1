using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp4.Models
{
    public class QuizManager
    {
        private List<Quiz> _quizzes = new List<Quiz>();

        public IReadOnlyList<Quiz> Quizzes => _quizzes.AsReadOnly();

        private readonly string _filePath;

        public QuizManager(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void AddQuiz(Quiz quiz)
        {
            if (quiz == null)
                throw new ArgumentNullException(nameof(quiz));

            _quizzes.Add(quiz);
        }

        public bool RemoveQuiz(Quiz quiz)
        {
            if (quiz == null)
                throw new ArgumentNullException(nameof(quiz));

            return _quizzes.Remove(quiz);
        }

        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                // Чтобы сериализатор мог корректно обрабатывать только свойства с приватными сеттерами:
                IncludeFields = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string json = JsonSerializer.Serialize(_quizzes, options);
            File.WriteAllText(_filePath, json);
        }

        public void Load()
        {
            if (!File.Exists(_filePath))
            {
                _quizzes = new List<Quiz>();
                return;
            }

            string json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            try
            {
                _quizzes = JsonSerializer.Deserialize<List<Quiz>>(json, options) ?? new List<Quiz>();
            }
            catch (JsonException)
            {
                // Можно обработать ошибку десериализации, например, очистить список или уведомить пользователя
                _quizzes = new List<Quiz>();
            }
        }
    }

}
