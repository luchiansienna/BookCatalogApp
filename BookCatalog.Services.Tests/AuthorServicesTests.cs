using AutoMapper;
using BookCatalog.Domain;
using BookCatalog.Repositories;
using BookCatalog.Repositories.Tests;
using BookCatalog.Services.DTO;
using BookCatalog.Services.Exceptions;
using BookCatalog.Services.UnitOfWork;
using FluentAssertions;
using Moq;

namespace BookCatalog.Services.Tests
{
    public class AuthorServicesTests
    {

        private AutoMapper.Mapper mapper;

        [SetUp]
        public void SetUp()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AuthorServices).Assembly);
            });
            mapper = new AutoMapper.Mapper(mapperConfig);
        }

        AuthorServices SetupService(List<Author> authorsList, List<Book> booksList = null)
        {
            var catalogContext = new Mock<BookCatalogContext>();
            var dbContextMock = DbContextMock.GetMock(catalogContext, authorsList, x => x.Author);
            if (booksList != null)
            {
                dbContextMock = DbContextMock.GetMock(dbContextMock, booksList, x => x.Book);
            }
            RepositoriesWrapper repositoriesWrapper = new RepositoriesWrapper(dbContextMock.Object);

            return new AuthorServices(repositoriesWrapper, mapper);
        }


        [Test]
        public void CreateAuthorService()
        {
            var author = new AuthorDTO()
            {
                Id = 1,
                Name = "John",
                Surname = "McGregor",
                Birthyear = 1986

            };
            var authorServices = SetupService(new List<Author>());
            var createdAuthor = authorServices.Create(author);

            var result = authorServices.GetById(createdAuthor?.Id ?? 0);

            Assert.Multiple(() =>
            {
                Assert.That(createdAuthor, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                result.Should().BeEquivalentTo(author);
            });
        }


        [Test]
        public void UpdateAuthorService()
        {
            var authorDTO = new AuthorDTO()
            {
                Id = 1,
                Name = "New name",
                Surname = "New surname",
                Birthyear = 1988

            };

            var authorList = new List<Author>();
            authorList.Add(new Author() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            var author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "McGregor",
                Birthyear = 1986
            };

            var authorServices = SetupService(new List<Author>() { author });
            var updatedAuthor = authorServices.Update(author.Id, authorDTO);

            var result = authorServices.GetById(updatedAuthor?.Id ?? 0);

            Assert.Multiple(() =>
            {
                Assert.That(updatedAuthor, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                result.Should().BeEquivalentTo(authorDTO);
            });
        }

        [Test]
        public void GetAuthorService()
        {
            var author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "McGregor",
                Birthyear = 1986

            };

            var authorServices = SetupService(new List<Author>() { author });
            var result = authorServices.GetById(author.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                result.Should().BeEquivalentTo(mapper.Map<AuthorDTO>(author));
            });
        }

        [Test]
        public void GetAllAuthorService()
        {
            var authorList = new List<Author>();
            authorList.Add(new Author() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });


            var authorServices = SetupService(authorList);
            var result = authorServices.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2));
                result.FirstOrDefault(x => x.Id == authorList[0].Id).Should().BeEquivalentTo(mapper.Map<AuthorDTO>(authorList[0]));
            });
        }

        [Test]
        public void DeleteAuthorService()
        {
            var authorList = new List<Author>();
            authorList.Add(new Author() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });


            var authorServices = SetupService(authorList, new List<Book>() { });
            var result = authorServices.Delete(1);

            var allRemainingAuthors = authorServices.GetAll();

            Assert.That(allRemainingAuthors.Count(), Is.EqualTo(1));
            allRemainingAuthors.FirstOrDefault().Should().BeEquivalentTo(mapper.Map<AuthorDTO>(authorList.FirstOrDefault(x => x.Id == 2)));
        }

        [Test]
        public void DeleteAuthorWithExistingBooksService()
        {
            var authorList = new List<Author>();
            authorList.Add(new Author() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            var book = new Book()
            {
                Id = 100,
                Title = "New Book",
                Authors = authorList,
                Publisher = new Publisher() { Id = 1, Name = "Star Publisher" },
                Edition = "First edition",
                PublishedDate = new DateOnly(2010, 1, 1)
            };

            var authorServices = SetupService(authorList, new List<Book>() { book });
            Assert.Throws<DependantItemsFoundException>(() => authorServices.Delete(1));

        }
    }
}