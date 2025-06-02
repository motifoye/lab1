namespace WpfApp4.Models
{
    // Класс Результата
    public class Result
    {
        public int CorrectAnswers { get; private set; }
        public int IncorrectAnswers { get; private set; }

        public Result(int correctAnswers, int incorrectAnswers)
        {
            if (correctAnswers < 0) throw new ArgumentOutOfRangeException(nameof(correctAnswers));
            if (incorrectAnswers < 0) throw new ArgumentOutOfRangeException(nameof(incorrectAnswers));

            CorrectAnswers = correctAnswers;
            IncorrectAnswers = incorrectAnswers;
        }

        public int Total => CorrectAnswers + IncorrectAnswers;

        public double Percentage => Total == 0 ? 0 : (double)CorrectAnswers / Total * 100;
    }
}
