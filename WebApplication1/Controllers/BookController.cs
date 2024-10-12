using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class BookController : Controller
    {
        private readonly CoreDbContext _coreDbContext;
        public BookController(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        // GET: book/list
        [HttpGet]
        [Route("book/list")]
        public List<Book> List()
        {
            return _coreDbContext.Set<Book>().ToList();
        }

        // GET: book/detail/{id}
        [HttpGet]
        [Route("book/detail/{id}")]
        public Book Detail(int id)
        {
            return _coreDbContext.Set<Book>().Single(x => x.Id == id);
        }

        // POST: book/add
        [HttpPost]
        [Route("book/create")]
        public async Task<ActionResult<Book>> Create(Book book) { 
            _coreDbContext.Book.Add(book);
            await _coreDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Detail), new { id = book.Id });
        }

        [HttpPut("{id}")]
        [Route("book/save")]
        public async Task<IActionResult> Save(int id, Book book) { 
            if(id != book.Id)
            {
                return BadRequest();
            }
            _coreDbContext.Entry(book).State = EntityState.Modified;
            _coreDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Route("book/delete")]
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
        //// GET: BookController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BookController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: BookController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BookController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: BookController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: BookController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
