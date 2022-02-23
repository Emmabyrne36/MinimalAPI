using MinimalAPI.Models;

namespace MinimalAPI
{
    // Use this class to simulate calling a DB
    public class DatabaseSimulator
    {
        private readonly List<Book> _bookList = new()
        {
            new Book(1, "Harry Potter and the Philosopher's Stone", 1997, new DateTime(1997, 06, 26), 15),
            new Book(2, "Harry Potter and the Chamber of Secrets", 1998, new DateTime(1998, 07, 02), 15),
            new Book(3, "Lord of the Rings", 1950, new DateTime(1954, 07, 29), 10),
            new Book(4, "Mistborn", 2010, new DateTime(2006, 07, 17), 20)
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
