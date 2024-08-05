using BookCatalog.Domain;
using BookCatalog.Services.Contracts;
using BookCatalog.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private readonly IBookServices _service;

        public BookController(ILogger<BookController> logger, IBookServices service)
        {
            _logger = logger;
            _service = service;

        }

        /// <summary>
        /// Get all the books
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var books = _service.GetAll();
            return Ok(books);
        }

        /// <summary>
        /// Get a book by its ID
        /// </summary>
        /// <param name="id">The ID of the book</param>
        [HttpGet("{bookId}", Name = nameof(GetBookById))]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetBookById(int bookId)
        {
            var book = _service.GetById(bookId);
            return Ok(book);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="book">The book to create</param>
        [HttpPost]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] BookDTO? book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            var createdBook = _service.Create(book);
            return CreatedAtAction(nameof(GetBookById), new { bookId = createdBook.Id }, createdBook);
        }

        /// <summary>
        /// Update the information for an existing book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book data.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] BookDTO? book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            var resultedBook = _service.Update(id, book);
            return Ok(resultedBook);
        }

        /// <summary>
        /// Remove a book by its id
        /// </summary>
        /// <param name="id">The ID of the book to delete</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletedBook = _service.Delete(id);
            return Ok(deletedBook);
        }

    }
}
