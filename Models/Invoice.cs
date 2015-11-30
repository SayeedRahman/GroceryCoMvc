using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroceryCoMvc.Models;


namespace GroceryCoMvc.Models
{
    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime? Date { get; set; }
        public string CompanyName { get; set; }
        public int? AddressId { get; set; }        

        [Column(TypeName = "money")]
        public decimal? TotalDiscount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public string InvoiceString { get; set; }

        [NotMapped]
        public List<ShoppingCartItem> CartItemList { get; set; }

        public Invoice()
        {
            Date = DateTime.Now;
            CartItemList = new List<ShoppingCartItem>() { };
        }
    }
}
