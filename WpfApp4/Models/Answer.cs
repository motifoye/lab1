namespace WpfApp4.Models
{
    // Класс Ответа
    public class Answer
    {
        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }

        public Answer(string text, bool isCorrect)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            IsCorrect = isCorrect;
        }
    }
}
