using BookCatalog.Domain;

namespace BookCatalog.Repositories.Contracts
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public bool IsAuthorAlreadyExisting(string name, string surname);

        public bool AreExisingBooksWithAuthor(int authorId);
    }
}
