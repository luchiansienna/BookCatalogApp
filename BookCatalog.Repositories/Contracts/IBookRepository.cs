using BookCatalog.Domain;

namespace BookCatalog.Repositories.Contracts
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public bool IsBookAlreadyExisting(string title);
        public void ClearAuthors(Book book);
    }
}
