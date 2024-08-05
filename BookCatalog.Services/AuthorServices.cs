using AutoMapper;
using BookCatalog.Domain;
using BookCatalog.Services.Contracts;
using BookCatalog.Services.DTO;
using BookCatalog.Services.Exceptions;
using BookCatalog.Services.UnitOfWork;

namespace BookCatalog.Services
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IRepositoriesWrapper _repositoriesWrapper;
        private readonly IMapper _mapper;

        public AuthorServices(IRepositoriesWrapper repositoriesWrapper, IMapper mapper)
        {
            _repositoriesWrapper = repositoriesWrapper;
            _mapper = mapper;
        }
        public AuthorDTO GetById(int id)
        {
            var author = _repositoriesWrapper.Authors.Get(id);
            if (author == null)
            {
                throw new DbEntityNotFoundException($"Author with id {id} not found int the database.");
            }
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }
        public IEnumerable<AuthorDTO> GetAll()
            => _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(_repositoriesWrapper.Authors.GetAll());

        public AuthorDTO Create(AuthorDTO authorDTO)
        {
            var alreadyExisting = _repositoriesWrapper.Authors.IsAuthorAlreadyExisting(authorDTO.Name, authorDTO.Surname);

            if (alreadyExisting)
            {
                throw new ItemAlreadyExistsException($"An author with name: '{authorDTO.Name} {authorDTO.Surname}' already exists in database!");
            }

            var author = _mapper.Map<Author>(authorDTO);
            var resultedAuthor = _repositoriesWrapper.Authors.Save(author);
            return _mapper.Map<AuthorDTO>(resultedAuthor);
        }
        public AuthorDTO Update(int id, AuthorDTO authorDTO)
        {
            var authorToUpdate = _repositoriesWrapper.Authors.Get(id);

            if (authorToUpdate == null)
            {
                throw new DbEntityNotFoundException($"Author with id {id} not found in the database.");
            }

            _mapper.Map(authorDTO, authorToUpdate);
            var resultedAuthor = _repositoriesWrapper.Authors.Update(authorToUpdate);
            return _mapper.Map<AuthorDTO>(resultedAuthor);
        }
        public AuthorDTO Delete(int id)
        {
            var author = _repositoriesWrapper.Authors.Get(id);

            if (author == null)
            {
                throw new DbEntityNotFoundException($"Author with id {id} is not found in the database.");
            }
            if (_repositoriesWrapper.Authors.AreExisingBooksWithAuthor(id))
            {
                throw new DependantItemsFoundException($"Cannot delete author with id {id}. This author has books in the database.");
            }

            var resultedAuthor = _repositoriesWrapper.Authors.Remove(author);
            return _mapper.Map<AuthorDTO>(resultedAuthor);
        }
    }
}
