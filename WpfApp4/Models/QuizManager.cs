using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp4.Models
{
    public class QuizManager(string filePath)
    {
        private List<Quiz> _quizzes = [];
        private readonly JsonSerializerOptions opts = new()
        {
            WriteIndented = true,
        };

        public IReadOnlyList<Quiz>? Quizzes => _quizzes.AsReadOnly();

        private readonly string _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

        public void AddQuiz(Quiz quiz)
        {
            ArgumentNullException.ThrowIfNull(quiz);

            _quizzes.Add(quiz);
        }

        public bool RemoveQuiz(Quiz quiz)
        {
            ArgumentNullException.ThrowIfNull(quiz);

            return _quizzes.Remove(quiz);
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(_quizzes, opts);
            File.WriteAllText(_filePath, json);
        }

        public void Load()
        {
            if (!File.Exists(_filePath))
            {
                _quizzes = [];
                return;
            }

            string json = File.ReadAllText(_filePath);

            try
            {
                _quizzes = JsonSerializer.Deserialize<List<Quiz>>(json, opts) ?? [];
            }
            catch (JsonException)
            {
                _quizzes = [];
            }
        }
    }

}
