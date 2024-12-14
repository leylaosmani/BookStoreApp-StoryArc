using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreApp.Models;

namespace BookStoreApp.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            using (var context = new BookStoreContext())
            {
                var books = context.Books.Include(b => b.Genre).ToList();
                return View(books);
            }
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
            {
                using (var context = new BookStoreContext())
                {
                    var book = context.Books.Find(id);
                    if (book == null)
                    {
                        return HttpNotFound();
                    }
                    return View(book); // Pass the book object to the view
                }
            }

            // GET: Book/Create
            public ActionResult Create()
        {
            using (var context = new BookStoreContext())
            {
                ViewBag.Genres = new SelectList(context.Genretbl.ToList(), "Id", "Name");
            }
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Price,ISBN,GenreId")] Booktbl book)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BookStoreContext())
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            using (var context = new BookStoreContext())
            {
                ViewBag.Genres = new SelectList(context.Genretbl.ToList(), "Id", "Name", book.GenreId);
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var context = new BookStoreContext())
            {
                
                Booktbl booktbl = context.Books.Find(id);
                if (booktbl == null)
                {
                    return HttpNotFound();
                }

                
                ViewBag.GenreId = new SelectList(context.Genretbl.ToList(), "Id", "Name", booktbl.GenreId);
                return View(booktbl);
            }
        }


        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Price,ISBN,GenreId")] Booktbl book)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BookStoreContext())
                {
                    
                    context.Entry(book).State = EntityState.Modified;
                    context.SaveChanges();
                }

                // Redirect to Index if everything is successful
                return RedirectToAction("Index");
            }

            
            using (var context = new BookStoreContext())
            {
                ViewBag.GenreId = new SelectList(context.Genretbl.ToList(), "Id", "Name", book.GenreId);
            }

            return View(book); 
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new BookStoreContext())
            {
                var book = context.Books.Find(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var context = new BookStoreContext())
            {
                var book = context.Books.Find(id);
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Search(string query)
        {
            using (var context = new BookStoreContext())
            {
                var books = context.Books.Include("Genre")
                    .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
                    .ToList();

                return PartialView("_BookListPartial", books);
            }
        }





    }

}
