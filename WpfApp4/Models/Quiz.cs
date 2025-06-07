namespace WpfApp4.Models
{
    public class Quiz
    {
        //private uint id;
        //private static uint lastId = 0;

        //public Quiz()
        //{
        //    Id = ++lastId;
        //}

        //public uint Id
        //{
        //    get => id;
        //    private set => id = value;
        //}
        public string? Title { get; set; }
        public List<Question> Questions { get; set; } = [];
    }
}
