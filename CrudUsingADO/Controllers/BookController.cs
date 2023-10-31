using CrudUsingADO.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudUsingADO.Controllers
{
    public class BookController : Controller
    {
        BookDAL db;
        IConfiguration configuration;
        public BookController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new BookDAL(this.configuration);
        }
        // GET: BookController
        public ActionResult Index()
        {

            return View(db.GetBooks());
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = db.GetBookById(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                int result = db.AddBook(book);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));//List
                }
                else
                {
                    return View();//remain on create page
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = db.GetBookById(id);
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            try
            {
                int result = db.UpdateBook(book);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));//List
                }
                else
                {
                    return View();//remain on Edit page
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = db.GetBookById(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteBook(id);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));//List
                }
                else
                {
                    return View();//remain on Edit page
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
