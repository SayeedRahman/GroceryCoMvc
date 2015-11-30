using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.Models
{
    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public DateTime? Date { get; set; }
        public int? ProductId { get; set; }
        public int? PricingRuleId { get; set; }
        public int? Quantity { get; set; }
                
        [Column(TypeName = "money")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public int? TotalQuantity { get; set; }
        [NotMapped]
        public int? DiscountedQuantity { get; set; }
        [NotMapped]
        public int? FreeQuantity { get; set; }
        [NotMapped]
        public int? BoughtQuantity { get; set; }
        [NotMapped]
        public int? BuyQuantityForCount { get; set; } /* counter for BuyQuantityFor */


        public ShoppingCartItem()
        {
            Date = DateTime.Now;
            Product = new Product() { };
            FreeQuantity = 0;
            TotalQuantity = 0;
            BoughtQuantity = 0;
            DiscountedQuantity = 0;
        }
    }
}
