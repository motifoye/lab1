namespace WpfApp4.Models
{
    public class Answer
    {
        private uint id;
        private static uint lastId = 0;

        public Answer()
        {
            Id = ++lastId;
        }

        public uint Id
        {
            get => id;
            private set => id = value;
        }
        public string? Text { get; set; }
        public bool IsCorrect { get; set; } = false;
    }
}
