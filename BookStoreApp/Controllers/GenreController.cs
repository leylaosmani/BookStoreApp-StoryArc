using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreApp.Models;

namespace BookStoreApp.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        public ActionResult Index()
        {
            using (var context = new BookStoreContext())
            {
                var genres = context.Genretbl.ToList();
                return View(genres);
            }
        }

        // GET: Genre/Details/5
        public ActionResult Details(int id)
        {
            using (var context = new BookStoreContext())
            {
                var genre = context.Genretbl.Find(id);
                if (genre == null)
                {
                    return HttpNotFound(); 
                }
                return View(genre);
            }
        }

        

    }
}

