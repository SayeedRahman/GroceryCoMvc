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
    public enum DiscountRuleEnum
    {
        Regular, BuyQuantityFor, BuyOneGetFree, BuyOneGetDiscount, BuyQuantityGetFree, BuyQuantityGetDiscount,  DiscountPercent,
    }

    [Table("PricingRule")]
    public partial class PricingRule
    { 
        [Key]
        public int PricingRuleId { get; set; }
        public int? PromotionId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ExpDate { get; set; } /* Expiration Data */
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; } /* if applicable, will simply hold the formula*/
        public decimal? FormulaValue { get; set; } /* value produced by the formula */
        public int? BuyQuantity { get; set; } /* for BuyQuantityGet */
        public int? DiscountQuantity { get; set; } /* for BuyQuantityGet */

        public DiscountRuleEnum? DiscountRuleEnumType { get; set; }

        public int? DiscountPercent { get; set; }

        /* If the DiscountPercent is NOT used, instead a set price is provided by the pricing rule, this field is used*/
        public decimal? DiscountedPrice { get; set; } /* it's a set dollar amount as the final price of the product */
        public decimal? DiscountedAmount { get; set; } /* set dollar amount off of the original price */

        public IEnumerable<SelectListItem> DiscountRuleEnumTypeSelectList { get; set; }

        public PricingRule()
        {

            BuyQuantity = 0;
            
            Date = DateTime.Now;
            DiscountRuleEnumType = DiscountRuleEnum.Regular;         
            DiscountPercent = 0;
            DiscountedPrice = 0;
            DiscountedAmount = 0;
            DiscountRuleEnumTypeSelectList = new List<SelectListItem>();
        }
    }
}
