using BookLibraryApi.Base;
using BookLibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public BooksController() { }
        /// <summary>
        /// Gets a list of all books.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of books available in the system.
        /// </remarks>
        /// <returns>A list of bookss.</returns>
        [HttpGet("getAllBooks")]

        [SwaggerOperation(Summary = "Get all books", Description = "Returns a list of all books.")]
        [SwaggerResponse(200, "Success", typeof(List<Book>))]

        public ActionResult<List<Book>> GetBooks()
        {

            var result = StaticDB.Books.ToList();
            return Ok(result);

        }


        [HttpGet("getBookById")]
        public ActionResult<Book> GetBookById(int? id)
        {
            if (id == null || id < 0 || id > StaticDB.Books.Count)
                return BadRequest();

            Book result = StaticDB.Books[(int)id];

            return Ok(result);

        }


        [HttpGet("findBook/{author}/{title}")]
        public ActionResult<Book> FindBook(string author, string title)
        {
            if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
            {
                return BadRequest("Enter author and title of a book");
            }
            var result = StaticDB.Books.Where(b => b.Author == author && b.Title == title).FirstOrDefault();
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost("addBook")]
        public ActionResult AddBook([FromBody] Book item)
        {

            if (item == null)
                return BadRequest("The provided data can not be null");

            StaticDB.Books.Add(item);

            return Ok();
        }

        [HttpPost("listAllBooksByTitle")]
        public ActionResult<List<string>> ReturnBookTitles([FromBody] List<Book> bookList)
        {

            if (bookList == null)
                return BadRequest("The provided list of books is null or empty.");

            List<string> bookTitles = bookList.Select(book => book.Title).ToList();

            return Ok(bookTitles);

        }

    }
}
