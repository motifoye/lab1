namespace WpfApp4.Models
{
    public class Result
    {
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }

        public int Total => CorrectAnswers + IncorrectAnswers;
        public double Percentage => Total == 0 ? 0 : (double)CorrectAnswers / Total * 100;
    }
}
