namespace WpfApp4.Models
{
    // Класс Вопроса
    public class Question
    {
        public string Text { get; private set; }
        private List<Answer> _answers;

        public IReadOnlyList<Answer> Answers => _answers.AsReadOnly();

        public Question(string text, List<Answer> answers)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Question text cannot be empty.", nameof(text));
            if (answers == null || answers.Count < 2)
                throw new ArgumentException("There must be at least two answers.", nameof(answers));
            if (!answers.Any(a => a.IsCorrect))
                throw new ArgumentException("There must be at least one correct answer.", nameof(answers));

            Text = text;
            _answers = new List<Answer>(answers);
        }

        public bool CheckAnswer(int answerIndex)
        {
            if (answerIndex < 0 || answerIndex >= _answers.Count)
                throw new ArgumentOutOfRangeException(nameof(answerIndex));

            return _answers[answerIndex].IsCorrect;
        }
    }
}
