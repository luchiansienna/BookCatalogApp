using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;

namespace BookCatalog.Services.UnitOfWork
{
    public interface IRepositoriesWrapper : IDisposable
    {
        IBookRepository Books { get; }

        IAuthorRepository Authors { get; }
    }
}
