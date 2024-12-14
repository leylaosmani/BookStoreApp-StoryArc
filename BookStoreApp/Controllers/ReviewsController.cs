using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class ReviewsController : Controller
    {
       
        private static List<Review> reviews = new List<Review>();

        private readonly BookStoreContext db = new BookStoreContext();

        // GET: Ratings
        public ActionResult Ratings()
        {
            var model = new Review();

            ViewBag.Books = db.Books.ToList(); // Pass book list to view
            return View(model);
        }

        // POST: Submit Review
        [HttpPost]
        public ActionResult SubmitReview(int bookId, string reviewerName, string reviewText, int rating)
        {
            // Find the book title from the book ID
            var book = db.Books.SingleOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                // Add review to the in-memory list
                reviews.Add(new Review
                {
                    BookId = bookId,
                    BookTitle = book.Title,
                    ReviewerName = reviewerName,
                    ReviewText = reviewText,
                    Rating = rating,
                    ReviewDate = DateTime.Now
                });

                return RedirectToAction("MyReviews");
            }

            ViewBag.ErrorMessage = "Book not found.";
            return RedirectToAction("Ratings");
        }


        // GET: MyReviews
        public ActionResult MyReviews()
        {
            return View(reviews); 
        }
    }
}