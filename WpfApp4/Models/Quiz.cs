namespace WpfApp4.Models
{
    // Класс Викторины
    public class Quiz
    {
        public string Title { get; private set; }
        private List<Question> _questions;

        public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

        public Quiz(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Quiz title cannot be empty.", nameof(title));

            Title = title;
            _questions = new List<Question>();
        }

        public void AddQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _questions.Add(question);
        }
    }
}
