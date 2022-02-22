using MinimalAPI.Models;

namespace MinimalAPI
{
    // Use this class as a simulation rather than querying an actual DB
    public class DatabaseSimulator
    {
        private List<Book> _bookList = new List<Book>
        {
            new Book(1, "Harry Potter 1", 1999, new DateTime(1999, 01, 01), 15),
            new Book(2, "Harry Potter 2", 1999, new DateTime(2000, 01, 01), 15),
            new Book(3, "Lord of the Rings", 1950, new DateTime(1950, 01, 01), 10),
            new Book(4, "Mistborn", 2010, new DateTime(2010, 01, 01), 20)
        };

        public async Task<Book> GetBook(int id)
        {
            await Task.Delay(500);

            return _bookList.FirstOrDefault(x => x.BookID == id);
        }

        public List<Book> QueryBooks(string query) 
            => _bookList.Where(x => x.Title.ToLower().Contains(query.ToLower())).ToList(); 

        public async Task<List<Book>> GetBooks()
        {
            await Task.Delay(500);

            return _bookList;
        }

        public async Task AddBook(Book newBook)
        {
            await Task.Delay(500);

            _bookList.Add(newBook);
        }
    }
}
