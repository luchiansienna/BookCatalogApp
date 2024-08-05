using BookCatalog.Domain;
using BookCatalog.Services.DTO;
using BookCatalog.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AuthorCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {

        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorServices _service;

        public AuthorController(ILogger<AuthorController> logger, IAuthorServices service)
        {
            _logger = logger;
            _service = service;

        }

        /// <summary>
        /// Get all the authors
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Author>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var authors = _service.GetAll();
            return Ok(authors);
        }

        /// <summary>
        /// Get a author by its ID
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        [HttpGet("{authorId}", Name = nameof(GetAuthorById))]
        [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetAuthorById(int authorId)
        {
            var author = _service.GetById(authorId);
            return Ok(author);
        }

        /// <summary>
        /// Create a new author
        /// </summary>
        /// <param name="author">The author to create.</param>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] AuthorDTO? author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            var createdAuthor = _service.Create(author);
            return CreatedAtAction(nameof(GetAuthorById), new { authorId = createdAuthor.Id }, createdAuthor);
        }

        /// <summary>
        /// Update the information for an existing author
        /// </summary>
        /// <param name="id">The ID of the author to update</param>
        /// <param name="author">The updated author data</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] AuthorDTO? author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            var resultedAuthor = _service.Update(id, author);
            return Ok(resultedAuthor);
        }

        /// <summary>
        /// Remove a author by its id
        /// </summary>
        /// <param name="id">The ID of the author to delete</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletedAuthor = _service.Delete(id);
            return Ok(deletedAuthor);
        }

    }
}
