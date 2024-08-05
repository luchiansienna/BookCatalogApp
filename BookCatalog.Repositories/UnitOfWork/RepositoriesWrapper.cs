using BookCatalog.Domain;
using BookCatalog.Repositories;
using BookCatalog.Repositories.Contracts;

namespace BookCatalog.Services.UnitOfWork
{
    public class RepositoriesWrapper : IRepositoriesWrapper
    {
        private readonly BookCatalogContext _dbContext;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private bool _isDisposed;

        public RepositoriesWrapper(BookCatalogContext dbContext) 
        { 
            _dbContext = dbContext;
            //Books = new BookRepository(_dbContext);
            //Authors = new AuthorRepository(_dbContext);
        }

        public virtual IBookRepository Books
        //{ get { return _bookRepository; } }
        => _bookRepository ??= new BookRepository(_dbContext);

        public virtual IAuthorRepository Authors
        //{ get { return _authorRepository; } }
        => _authorRepository ??= new AuthorRepository(_dbContext);

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _isDisposed = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
