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
    class ProcessCheckout
    {
        public UsersContext db = new UsersContext() { };
        public ProcessShoppingCart ProcessShoppingCart = new ProcessShoppingCart() { };
        public ProcessPricingRules ProcessPricingRules = new ProcessPricingRules() { };

        //--------------------------------------------------------
        public Invoice CreateInvoice(ref List<ShoppingCartItem> scList, ref List<ShoppingCartItem> gscList)
        {
            Invoice invoice = new Invoice() { };

            if (scList != null  && gscList != null)
            {
                ProcessShoppingCart.ProcessCart(ref scList, ref gscList);

                invoice.CartItemList = gscList;
            }

            invoice.CompanyName = "GroceryCo";
            invoice.InvoiceString = CreateReceiptString(invoice, ref gscList);
            //invoice = CalculateInvoice(invoice);

            db.Invoices.Add(invoice);
            db.SaveChanges();

            return invoice;
        }
        //--------------------------------------------------------
        public string CreateReceiptString(Invoice invoice, ref List<ShoppingCartItem> gscList)
        {
            int itemNo = 1;
            string receiptString = null;
            PricingRule pr = null;
            if (invoice != null)
            {
                receiptString = "\t\t\t" +invoice.CompanyName + "\t\t\n";
                receiptString += "\t\t\t9999 Any Stree NW\t\t\n";
                receiptString += "\t\t\tCalgary, AB T9T T9T\t\t\n";
                receiptString += "\t\t\tCanada\t\t\n";
                receiptString += "\t\t\tTel: 403-403-4040\t\t\n";
                receiptString += "\t\t\tDate: " + DateTime.Now +"\t\t\n";
                receiptString += "----------------------------------------------------------------------\n";
                if (gscList != null && gscList.Count() > 0)
                {
                    foreach (var item in gscList)
                    {                      
                        receiptString += itemNo + ".\t" + item.Product.Name + "\t" + item.Total + "\n";

                        pr = db.PricingRules.Find(item.PricingRuleId);
                        receiptString += "\t" + (itemNo++) + "\t" + pr.Name + "\t";
                        if (item.Discount > 0)
                            receiptString += item.Discount + "\n";
                    }
                    receiptString +=  "----------------------------------------------------------------------\n";
                    receiptString += "\tTotalDiscount: " + invoice.TotalDiscount + "\tTotal: " + invoice.Total + "\n";
                }
            }

            return receiptString;
        }
        //--------------------------------------------------------
        public Invoice CalculateInvoice(Invoice invoice)
        {
            if (invoice != null)
            {
                decimal total = 0, discount = 0;
                foreach (var item in invoice.CartItemList)
                {
                    total = (decimal)(total + item.Total);
                    discount = (decimal)(discount + item.Discount);
                }
                invoice.Total = total;
                invoice.TotalDiscount = discount;
            }
            return invoice;
        }
        //--------------------------------------------------------
        
    }
}
