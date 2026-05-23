using BookLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Angels & Demons", Author = "Dan Brown", Genre = "Techno-Thriller" , Availability = true, PublishedYear = 2004 },
            new Book { Id = 1, Title = "My Life", Author = "Bill Clinton", Genre = "Political Nonfiction" , Availability = true, PublishedYear = 2004 },
        };

        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Book Retrieved" });
        }

        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });
            return Ok(new { status = "success", data = book, message = "Book Retrieved" });
        }

        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, 
            new { status = "success", data = newBook, message = "Book Created" });
        }

        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Availability = updatedBook.Availability;
            book.PublishedYear = updatedBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book Updated" });
        }

        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

            books.Remove(book);
            return Ok(new { status = "success", data = (object?)null, message = "Book Deleted" });
        }
    }
}