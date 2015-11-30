using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroceryCoMvc.Models;


namespace GroceryCoMvc.Models
{
    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        public DateTime? Date { get; set; }

        [NotMapped]
        public List<ShoppingCartItem>  CartItemList { get; set; }
        [NotMapped]
        public List<int> ProductList { get; set; } /* unsorted list of items */

        public ShoppingCart()
        {
            Date = DateTime.Now;
            CartItemList = new List<ShoppingCartItem>() { };
            ProductList = new List<int>() { };
        }
    }
}
