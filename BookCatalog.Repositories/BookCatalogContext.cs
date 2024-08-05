using BookCatalog.Domain;
using Microsoft.EntityFrameworkCore;


namespace BookCatalog.Repositories
{
    public class BookCatalogContext : DbContext
    {
        public BookCatalogContext() : base() { }
        public BookCatalogContext(DbContextOptions<BookCatalogContext> options) : base(options) { }
        public virtual DbSet<Book> Book { get; set; } = null!;

        public virtual DbSet<Author> Author { get; set; } = null!;

        public virtual DbSet<Publisher> Publisher { get; set; } = null!;
    }
}
