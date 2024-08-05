using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;

namespace BookCatalog.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookCatalogContext context) : base(context) { }

        public bool IsAuthorAlreadyExisting(string name, string surname) => _dbSet.Any(x => x.Name == name && x.Surname == surname);

        public bool AreExisingBooksWithAuthor(int authorId) => _context.Book.Any(x => x.Authors.Any( author => author.Id == authorId));
    }
}
