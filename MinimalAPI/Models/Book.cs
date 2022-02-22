namespace MinimalAPI.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public long ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public short Price { get; set; }
        public int AuthorID { get; set; }

        public Book(int bookId, string title, int year, DateTime publishedDate, short price)
        {
            BookID = bookId;
            Title = title;
            Year = year;
            PublishedDate = publishedDate;
            Price = price;
        }
    }
}
