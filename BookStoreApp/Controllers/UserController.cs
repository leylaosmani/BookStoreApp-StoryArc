using System;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreApp.Models;
using System.Diagnostics;


namespace BookStoreApp.Controllers
{
    public class UserController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Register
        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Register(Usertbl user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BookStoreContext()) // New context instance
                {
                    // Hash the password
                    user.Password = HashPassword(user.Password);

                    // Save the user
                    context.Usertbl.Add(user);
                    Debug.WriteLine($"Saving user: {user.Username}");
                    context.SaveChanges();
                }

                Session["Username"] = user.Username;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
            }

            return View(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var builder = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputPassword));
                var hash = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                return hash == storedHash;
            }
        }

        // GET: Login
        public ActionResult Login()
        {
            return View(new Usertbl());
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Usertbl.SingleOrDefault(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.Password))
            {
                
                Session["Username"] = user.Username;
                return RedirectToAction("Index", "Home");
            }

          
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        [HttpPost]
        public ActionResult Submit(Usertbl user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Form submitted successfully!";
                return View("Success");
            }
            return View("Register", user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


    }
}
































//// GET: User/Details/5
//public ActionResult Details(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    User user = db.Users.Find(id);
//    if (user == null)
//    {
//        return HttpNotFound();
//    }
//    return View(user);
//}

//// GET: User/Edit/5
//public ActionResult Edit(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    User user = db.Users.Find(id);
//    if (user == null)
//    {
//        return HttpNotFound();
//    }
//    return View(user);
//}

//// POST: User/Edit/5
//// To protect from overposting attacks, enable the specific properties you want to bind to, for 
//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Edit([Bind(Include = "Id,Username,Password,Email")] User user)
//{
//    if (ModelState.IsValid)
//    {
//        db.Entry(user).State = EntityState.Modified;
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    return View(user);
//}

//// GET: User/Delete/5
//public ActionResult Delete(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    User user = db.Users.Find(id);
//    if (user == null)
//    {
//        return HttpNotFound();
//    }
//    return View(user);
//}

//// POST: User/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public ActionResult DeleteConfirmed(int id)
//{
//    User user = db.Users.Find(id);
//    db.Users.Remove(user);
//    db.SaveChanges();
//    return RedirectToAction("Index");
//}

//protected override void Dispose(bool disposing)
//{
//    if (disposing)
//    {
//        db.Dispose();
//    }
//    base.Dispose(disposing);
//}
//    }
//}
