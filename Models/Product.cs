using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroceryCoMvc.Models;


namespace GroceryCoMvc.Models
{
    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int? PricingRuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [NotMapped]
        public DiscountRuleEnum? DiscountRuleEnumType { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> DiscountRuleEnumTypeSelectList { get; set; }
        
        public Product()
        {
            Price = 0;
            DiscountRuleEnumTypeSelectList = new List<SelectListItem>();
        }
    }
}
