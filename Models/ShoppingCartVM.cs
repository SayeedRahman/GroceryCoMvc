using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.Models
{
    public class ShoppingCartVM
    {
        public Invoice Invoice { get; set; }
        public PricingRule PricingRule { get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }

        public List<Product> ProductList { get; set; }
        public List<PricingRule> ShoppingCartPricingRuleList { get; set; }
        public List<ShoppingCartItem> CartItemList { get; set; }

        public ShoppingCartVM()
        {
            Invoice = new Invoice() { };
            PricingRule = new PricingRule();
            ShoppingCartItem = new ShoppingCartItem();

            ProductList = new List<Product>() { };
            ShoppingCartPricingRuleList = new List<PricingRule>() { };
            CartItemList = new List<ShoppingCartItem>() { };
        }
    }
}