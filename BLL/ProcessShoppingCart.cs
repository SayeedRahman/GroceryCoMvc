using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.BLL
{
    class ProcessShoppingCart
    {
        public UsersContext db = new UsersContext() { };
        public ProcessPricingRules ProcessPricingRules = new ProcessPricingRules();

        //-----------------------------------------------
        public void GroupShoppingCartItems(ref List<ShoppingCartItem> cartItemList, ref List<ShoppingCartItem> gscList)
        {
            ShoppingCartItem gscItem = null;
            if (cartItemList != null && cartItemList.Count() > 0)
            {
                foreach (var item in cartItemList)
                {
                    gscItem = GetCartItem(ref gscList, item.ProductId);
                    if (gscItem == null)
                    {
                        gscItem = item;
                        gscList.Add(gscItem);
                    }
                    else
                    {
                        gscItem.Quantity++;
                    }
                }
            }
        }        
        //-----------------------------------------------
        public ShoppingCartItem GetCartItem(ref List<ShoppingCartItem> cartList, int? productId)
        {
            if (cartList != null && cartList.Count() > 0 && productId > 0)
            {
                foreach (var item in cartList)
                {
                    if (item.ProductId == productId) return item;
                }
            }
            return null;
        }
        //-----------------------------------------------
        public List<PricingRule> CreatePricingRuleList(List<ShoppingCartItem> scList)
        {
            PricingRule pr = null;
            List<PricingRule> prList = new List<PricingRule>();

            if (scList != null && scList.Count() > 0)
            {
                foreach (var item in scList)
                {
                    if (item.Product != null)
                    {
                        if (item.Product.PricingRuleId > 0)
                        {
                            pr = db.PricingRules.Find(item.Product.PricingRuleId);
                            if (pr != null && !ProcessPricingRules.IsFound(ref prList, pr.PricingRuleId))
                                prList.Add(pr);
                        }
                    }
                }
            }
            return prList;
        }
        //-----------------------------------------------
        public List<ShoppingCartItem> ProcessCart(ref List<ShoppingCartItem> scList, ref List<ShoppingCartItem> gscList)
        {           
            GroupShoppingCartItems(ref scList, ref gscList);
            List<PricingRule> prList = CreatePricingRuleList(gscList);

            ProcessPricingRules ProcessPricingRules = new ProcessPricingRules() { };
            ProcessPricingRules.CalculateCartItemTotal(ref prList, ref gscList);

            return gscList;
        }
        //-----------------------------------------------
    }
}
