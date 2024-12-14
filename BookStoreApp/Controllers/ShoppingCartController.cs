using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart); 
        }

        // POST: Add to Cart
        public ActionResult AddToCart(int id)
        {
            using (var context = new BookStoreContext())
            {
                var book = context.Books.Find(id);
                if (book == null) return HttpNotFound();

                
                var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

                
                var cartItem = cart.FirstOrDefault(c => c.Id == id);
                if (cartItem != null)
                {
                    cartItem.Quantity++; 
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Price = book.Price,
                        Quantity = 1
                    });
                }

                // Save the updated cart back to the session
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }


        // POST: Remove from Cart
        public ActionResult RemoveFromCart(int id)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                var cartItem = cart.FirstOrDefault(c => c.Id == id);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                }
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            // Get the cart from session
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // Calculate total amount
            decimal totalAmount = cart.Sum(item => item.Price * item.Quantity);

            // Create the CheckoutViewModel
            var checkoutModel = new CheckoutViewModel
            {
                CartItems = cart,
                CheckoutModel = new CheckoutModel
                {
                    TotalAmount = totalAmount
                }
            };

            // Pass the model to the view
            return View(checkoutModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                Session["Cart"] = null;

                
                TempData["SuccessMessage"] = "Your purchase was successful! Thank you for shopping with us.";

              
                return RedirectToAction("ThankYou");
            }

            
            return View(model);
        }

        public ActionResult ThankYou()
        {
            return View();
        }



    }
}