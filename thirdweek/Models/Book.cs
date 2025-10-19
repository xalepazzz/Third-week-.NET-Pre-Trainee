namespace thirdweek.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly PublishDate  { get; set; }
        public int AuthorId {  get; set; }

    }
}
