namespace WpfApp4.Models
{
    public class Question
    {
        private uint id;
        private static uint lastId = 0;

        public Question() 
        {
            Id = ++lastId;
        }

        public uint Id
        {
            get => id;
            private set => id = value;
        }
        public string? Text { get; set; }
        public List<Answer> Answers { get; set; } = []; 
    }
}
