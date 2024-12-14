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
    public class BuyBooksController : Controller
    {
        

        // GET: BuyBooks
        public ActionResult Index()
        {
            using (var context = new BookStoreContext())
            {
                var books = context.Books.Include(b => b.Genre).ToList();
                return View(books); 
            }
        }
    }
}