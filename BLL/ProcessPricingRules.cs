using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.BLL
{
    class ProcessPricingRules
    {
        public UsersContext db = new UsersContext() { };

        //--------------------------------------------------------
        public bool IsFound(ref List<PricingRule> pricingRuleList, int id)
        {
            if (pricingRuleList != null && pricingRuleList.Count() > 0 && id > 0)
            {
                foreach (var item in pricingRuleList)
                {
                    if (item.PricingRuleId == id) return true;
                }
            }
            return false;
        }
        //--------------------------------------------------------
        public bool IsValidPricingRule(PricingRule pricingRule)
        {
            DateTime now = DateTime.Now;
            if (pricingRule != null)
                return now > pricingRule.ExpDate ? true : false;
            return false;
        }
        //--------------------------------------------------------
        public PricingRule GetPricingRule(ref List<PricingRule> pricingRuleList, int? id)
        {
            if (pricingRuleList != null && pricingRuleList.Count() > 0 && id > 0)
            {
                foreach (var item in pricingRuleList)
                {
                    if (item.PricingRuleId == id) return item;
                }
            }
            return null;
        }
        //--------------------------------------------------------
        public decimal ComputeBuyQuantityDiscount( PricingRule pr, ShoppingCartItem scItem)
        {
            int discountCount = 0;
            decimal discount = 0;
            int discountMuliplier = 1;
            if ((pr != null) && scItem != null && (scItem.Quantity > 0) &&
                (   (pr.DiscountRuleEnumType == DiscountRuleEnum.BuyOneGetFree) || 
                    (pr.DiscountRuleEnumType == DiscountRuleEnum.BuyQuantityGetFree) ||
                    (pr.DiscountRuleEnumType == DiscountRuleEnum.BuyOneGetDiscount) ||
                    (pr.DiscountRuleEnumType == DiscountRuleEnum.BuyQuantityGetDiscount)
                ) 
               )
            {
                int tempMultiplier = (int)scItem.Quantity / (int)(pr.BuyQuantity + pr.DiscountQuantity);
                discountMuliplier = (tempMultiplier == 0) ? 1 : tempMultiplier;

                int tempCount = (int)(scItem.Quantity - pr.BuyQuantity);
                if(pr.DiscountQuantity > 0)
                    discountCount = tempCount > (int)pr.DiscountQuantity ? (int)pr.DiscountQuantity : tempCount;
                
                if (discountCount > 0)
                    discount = (decimal)(scItem.Product.Price * (pr.DiscountPercent) * discountCount * discountMuliplier)/100;
            }
            else if ((pr != null) && (pr.DiscountRuleEnumType == DiscountRuleEnum.DiscountPercent))
                discount = discount + (decimal)(scItem.Product.Price * pr.DiscountPercent/100);
            //else if ((pr != null) && (pr.DiscountRuleEnumType == DiscountRuleEnum.DiscountedAmount))
                //discount = discount + (decimal)pr.DiscountedAmount;
                
            return discount;
        }
        //--------------------------------------------------------
        public void CalculateCartItemTotal(ref List<PricingRule> prList,  ref List<ShoppingCartItem> gscList)
        {
            //to keep track for item for BuyQuantityFor pricing rule
            int BuyQuantityForIndex = 0, i = 0;
            PricingRule buyQpr = null;

            if (prList != null && gscList != null)
            {
                foreach (var item in gscList)
                {
                    PricingRule pr = GetPricingRule(ref prList, item.Product.PricingRuleId);
                    item.Discount = ComputeBuyQuantityDiscount(pr, item);

                    if (pr.DiscountRuleEnumType == DiscountRuleEnum.BuyQuantityFor)
                    {
                        //to keep track for item for BuyQuantityFor pricing rule
                        BuyQuantityForIndex = i;
                        buyQpr = pr;

                        if (item.Quantity < pr.BuyQuantity)
                            item.Total = item.Product.Price * item.Quantity;
                        else
                        {
                            int tempMultiplier = (int)item.Quantity / (int)pr.BuyQuantity;
                            int discountMuliplier = (tempMultiplier == 0) ? 1 : tempMultiplier;
                            item.Total = pr.DiscountedPrice * discountMuliplier;
                        }

                        if (item.Quantity % pr.BuyQuantity > 0)
                        {
                           // ProcessBuyQuantityForRemainder(ref gscList, item, (int)(item.Quantity % pr.BuyQuantity));
                           //cannot update the gscList while foreach-loop is executing. So, must be done afterwards
                            item.Quantity = item.Quantity % pr.BuyQuantity;
                            item.BoughtQuantity = pr.BuyQuantity;
                        }
                    }
                    else item.Total = item.Product.Price * item.Quantity - item.Discount;

                    i++;
                }
                //Now check for DiscountRuleEnum.BuyQuantityFor Remainder
                if(gscList[BuyQuantityForIndex].Quantity > 0)
                    ProcessBuyQuantityForRemainder(ref gscList, gscList[BuyQuantityForIndex]);
            }
        }
        //--------------------------------------------------------
        public void ProcessBuyQuantityForRemainder(ref List<ShoppingCartItem> gscList, ShoppingCartItem scItem)
        {
            if (gscList != null && scItem != null)
            {
                int i = (int)scItem.Quantity;
                while (i > 0)
                {
                    ShoppingCartItem item = new ShoppingCartItem();
                    item = CopyItem(scItem, item);
                    item.Total = item.Product.Price;
                    gscList.Add(item);
                    i--;
                }
            }
        }
        //-------------------------------------------------------- 
        public ShoppingCartItem CopyItem(ShoppingCartItem source, ShoppingCartItem destination)
        {
            destination.ShoppingCartItemId = source.ShoppingCartItemId;
            destination.Date = source.Date;
            destination.ProductId = source.ProductId;
            destination.PricingRuleId = source.PricingRuleId;
            destination.Discount = 0;
            destination.Quantity = 1;
            destination.Product = source.Product;
            return destination;
        }
        //-------------------------------------------------------- 
    }
}
