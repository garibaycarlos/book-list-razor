using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [ApiController]
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GETALL()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Book getBook = await _db.Book.FindAsync(id);

            if (getBook != null)
            {
                _db.Book.Remove(getBook);

                await _db.SaveChangesAsync();

                return Json(new { success = true, message = "The book was deleted" });
            }
            else
            {
                return Json(new { success = false, message = "Error while deleting the book" });
            }
        }
    }
}