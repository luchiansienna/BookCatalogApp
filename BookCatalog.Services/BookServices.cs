using AutoMapper;
using BookCatalog.Domain;
using BookCatalog.Services.Contracts;
using BookCatalog.Services.DTO;
using BookCatalog.Services.Exceptions;
using BookCatalog.Services.UnitOfWork;

namespace BookCatalog.Services
{
    public class BookServices : IBookServices
    {
        private readonly IRepositoriesWrapper _repositoriesWrapper;
        private readonly IMapper _mapper;

        public BookServices(IRepositoriesWrapper repositoriesWrapper, IMapper mapper)
        {
            _repositoriesWrapper = repositoriesWrapper;
            _mapper = mapper;
        }

        public BookDTO GetById(int id)
        {
            var book = _repositoriesWrapper.Books.Get(id);
            if (book == null)
            {
                throw new DbEntityNotFoundException($"Book with id {id} not found int the database.");
            }

            return _mapper.Map<BookDTO>(book);
        }
        public IEnumerable<BookDTO> GetAll()
            => _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(_repositoriesWrapper.Books.GetAll());


        public BookDTO Create(BookDTO bookDTO)
        {
            var alreadyExisting = _repositoriesWrapper.Books.IsBookAlreadyExisting(bookDTO.Title);

            if (alreadyExisting)
            {
                throw new ItemAlreadyExistsException($"A book with the title: '{bookDTO.Title}' already exists in database!");
            }

            var book = _mapper.Map<Book>(bookDTO);
            var resultedBook = _repositoriesWrapper.Books.Save(book);
            return _mapper.Map<BookDTO>(resultedBook);
        }

        public BookDTO Update(int id, BookDTO bookDTO)
        {
            var bookToUpdate = _repositoriesWrapper.Books.Get(id);

            if (bookToUpdate == null)
            {
                throw new DbEntityNotFoundException($"Book with id {id} not found in the database.");
            }

            _repositoriesWrapper.Books.ClearAuthors(bookToUpdate);

            var resultedBook = _repositoriesWrapper.Books.Update(_mapper.Map<Book>(bookDTO));

            return _mapper.Map<BookDTO>(resultedBook);
        }

        public BookDTO Delete(int id)
        {
            var book = _repositoriesWrapper.Books.Get(id);

            if (book == null)
            {
                throw new DbEntityNotFoundException($"Book with id {id} is not found in the database.");
            }

            var resultedBook = _repositoriesWrapper.Books.Remove(book);
            return _mapper.Map<BookDTO>(resultedBook);
        }
    }
}
