using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    [Table("Usertbl")]
    public class Usertbl
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, ErrorMessage ="Username cannot be longer than 25 characters")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Username can only contain letters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Password must contain at least one letter and one number")]
        public string Password { get; set; } 

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }


        
    }

}