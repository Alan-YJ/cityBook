using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiController]
    public class BookController : Controller
    {
        private readonly CoreDbContext _coreDbContext;
        public BookController(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        // GET: book/list
        [AllowAnonymous]
        [HttpGet]
        [Route("book/list")]
        public List<Book> List()
        {
            return _coreDbContext.Set<Book>().ToList();
        }

        // GET: book/{id}
        [AllowAnonymous]
        [HttpGet]
        [Route("book/{id}")]
        public async Task<ActionResult<Book>> Detail(int id)
        {
            var book = await _coreDbContext.Set<Book>().FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // POST: book/create
        [HttpPost]
        [Route("book/create")]
        public async Task<ActionResult<Book>> Create([FromBody]Book book) { 
            _coreDbContext.Book.Add(book);
            await _coreDbContext.SaveChangesAsync();
            return CreatedAtAction("Detail", new { id = book.Id }, book);
        }

        [HttpPut]
        [Route("book/{id}")]
        public async Task<IActionResult> Save(int id, Book book) { 
            if(id != book.Id)
            {
                return BadRequest();
            }
            _coreDbContext.Entry(book).State = EntityState.Modified;
            await _coreDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("book/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _coreDbContext.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _coreDbContext.Book.Remove(book);
            await _coreDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
