using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookCatalogContext context) : base(context) { }

        public override Book? Get(int id) => _context.Book.Include(x => x.Authors).Include(x => x.Publisher).FirstOrDefault(x => x.Id == id);

        public override IEnumerable<Book> GetAll() => _context.Book.Include(x => x.Authors).Include(x => x.Publisher).ToList();

        public void ClearAuthors(Book model)
        {
            // clearing the list
            var list = model?.Authors.ToList();
            model?.Authors.Clear();
            _context.Update(model);
            _context.SaveChanges();

            // detaching from entities
            foreach (var author in list)
            {
                _context.Entry(author).State = EntityState.Detached;
            }
            _context.Entry(model).State = EntityState.Detached;
            _context.Entry(model.Publisher).State = EntityState.Detached;

        }
        public bool IsBookAlreadyExisting(string title) => _dbSet.Any(x => x.Title == title);

    }
}
