using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public CheckoutModel CheckoutModel { get; set; }
    }
}