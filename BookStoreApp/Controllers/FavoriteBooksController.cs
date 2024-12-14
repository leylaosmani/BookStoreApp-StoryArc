using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class FavoriteBooksController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BookStoreContext"].ConnectionString;



        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase ImageFile, FavoriteBook model)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;

                // Handle the image file upload
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid() + "_" + ImageFile.FileName;
                    string filePath = Server.MapPath("~/Images/") + fileName;
                    ImageFile.SaveAs(filePath);
                    imagePath = "~/Images/" + fileName;
                }

                // Save data to the database using ADO.NET
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO FavoriteBooks (FirstName, LastName, ImagePath) VALUES (@FirstName, @LastName, @ImagePath)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Index
        public ActionResult Index()
        {
            List<FavoriteBook> favoriteBooks = new List<FavoriteBook>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM FavoriteBooks";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavoriteBook book = new FavoriteBook
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : null
                    };

                    favoriteBooks.Add(book);
                }

                reader.Close();
            }

            return View(favoriteBooks);
        }
    }
}