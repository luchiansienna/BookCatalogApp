using AutoMapper;
using BookCatalog.Domain;
using BookCatalog.Repositories;
using BookCatalog.Repositories.Tests;
using BookCatalog.Services.DTO;
using BookCatalog.Services.UnitOfWork;
using FluentAssertions;
using Moq;

namespace BookCatalog.Services.Tests
{
    public class BookServicesTests
    {

        private AutoMapper.Mapper mapper;

        [SetUp]
        public void SetUp()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(BookServices).Assembly);
            });
            mapper = new AutoMapper.Mapper(mapperConfig);
        }

        BookServices SetupService(List<Book> booksList)
        {
            var dbContextMock = DbContextMock.GetMock(new Mock<BookCatalogContext>(), booksList, x => x.Book);
            RepositoriesWrapper repositoriesWrapper = new RepositoriesWrapper(dbContextMock.Object);

            return new BookServices(repositoriesWrapper, mapper);
        }


        [Test]
        public void CreateBookService()
        {

            var authorList = new List<AuthorDTO>();
            authorList.Add(new AuthorDTO() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new AuthorDTO() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            var book = new BookDTO()
            {
                Id = 100,
                Title = "New Book",
                Authors = authorList,
                Publisher = new PublisherDTO() { Id = 1, Name = "Star Publisher" },
                Edition = "First edition",
                PublishedDate = new DateTime(2010, 1, 1)
            };
            var bookServices = SetupService(new List<Book>());
            var createdBook = bookServices.Create(book);

            var result = bookServices.GetById(createdBook?.Id ?? 0);

            Assert.Multiple(() =>
            {
                Assert.That(createdBook, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                result.Should().BeEquivalentTo(book);
            });
        }


        [Test]
        public void UpdateBookService()
        {
            var authorListDTO = new List<AuthorDTO>();
            authorListDTO.Add(new AuthorDTO() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorListDTO.Add(new AuthorDTO() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            var bookDTO = new BookDTO()
            {
                Id = 100,
                Title = "New Updated Book",
                Authors = authorListDTO,
                Publisher = new PublisherDTO() { Id = 1, Name = "Star Publisher" },
                Edition = "First edition",
                PublishedDate = new DateTime(2010, 1, 1)
            };

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

            var bookServices = SetupService(new List<Book>() { book });
            var updatedBook = bookServices.Update(book.Id, bookDTO);

            var result = bookServices.GetById(updatedBook?.Id ?? 0);

            Assert.Multiple(() =>
            {
                Assert.That(updatedBook, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                result.Should().BeEquivalentTo(bookDTO);
            });
        }

        [Test]
        public void GetBookService()
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

            var bookServices = SetupService(new List<Book>() { book });
            var result = bookServices.GetById(book.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                result.Should().BeEquivalentTo(mapper.Map<BookDTO>(book));
            });
        }

        [Test]
        public void GetAllBookService()
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

            var bookNo2 = new Book()
            {
                Id = 101,
                Title = "Second Book",
                Authors = authorList,
                Publisher = new Publisher() { Id = 1, Name = "Star Publisher" },
                Edition = "Second edition",
                PublishedDate = new DateOnly(2010, 1, 1)
            };

            var bookServices = SetupService(new List<Book>() { book, bookNo2 });
            var result = bookServices.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2));
                result.FirstOrDefault(x => x.Id == book.Id).Should().BeEquivalentTo(mapper.Map<BookDTO>(book));
            });
        }

        [Test]
        public void DeleteBookService()
        {
            var authorList = new List<Author>();
            authorList.Add(new Author() { Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            var bookList = new List<Book>();
            bookList.Add(new Book()
            {
                Id = 100,
                Title = "New Book",
                Authors = authorList,
                Publisher = new Publisher() { Name = "Star Publisher" },
                Edition = "First edition",
                PublishedDate = new DateOnly(2010, 1, 1)
            });
            var bookServices = SetupService(bookList);
            var result = bookServices.Delete(100);

            var countAllBooks = bookServices.GetAll().Count();

            Assert.That(countAllBooks, Is.EqualTo(0));
        }
    }
}