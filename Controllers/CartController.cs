using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryCoMvc.BLL;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.Controllers
{
    public class CartController : Controller
    {
        public List<ShoppingCartItem> scList = MvcApplication.scList;
        public List<ShoppingCartItem> gscList = MvcApplication.gscList;
        public List<PricingRule> prList = MvcApplication.prList;  
        public UsersContext db = new UsersContext() { };

        //--------------------------------------
        //
        // GET: /ShoppingCart/Receipt

        public ActionResult Receipt()
        {
            ProcessCheckout ProcessCheckout = new ProcessCheckout(){};
            scList = new List<ShoppingCartItem>() { };
            gscList = new List<ShoppingCartItem>() { };

            scList = db.ShoppingCartItems.ToList();
            foreach (var item in scList)
            {
                item.Product = db.Products.Find(item.ProductId);
            }
            Invoice invoice = new Invoice() { };
            invoice = ProcessCheckout.CreateInvoice(ref scList, ref gscList);
            invoice.InvoiceString = ProcessCheckout.CreateReceiptString(invoice, ref gscList);
            return View(invoice);
        }
        //--------------------------------------
        //
        // GET: /ShoppingCart/Index

        public ActionResult Index()
        {
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM() { };

            shoppingCartVM.ProductList = db.Products.ToList();
            shoppingCartVM.CartItemList = db.ShoppingCartItems.ToList();
            foreach (var item in shoppingCartVM.CartItemList)
            {
                item.Product = db.Products.Find(item.ProductId);
            }
            shoppingCartVM.ShoppingCartPricingRuleList = prList;

            return View(shoppingCartVM);
        }
        //--------------------------------------
        //
        // POST: /ShoppingCart/Index

        public ActionResult AddToCart(int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ShoppingCartVM scVM = new ShoppingCartVM() { };
                    if (id > 0)
                    {
                        Product product = db.Products.Find(id);
                        if (product != null)
                        {                            
                            scVM.ShoppingCartItem.ProductId = product.ProductId;
                            scVM.ShoppingCartItem.PricingRuleId = db.PricingRules.Find(product.PricingRuleId).PricingRuleId;
                        }
                        scVM.ShoppingCartItem.Quantity = 1;                                               
                        
                        db.ShoppingCartItems.Add(scVM.ShoppingCartItem);
                        db.SaveChanges();                        
                    }                  
                }
                return RedirectToAction("Index");

                //------------------------------
            }
            catch
            {
                return View();
            }
        }
        //--------------------------------------
        private List<PricingRule> CreateShopperPricingRuleList()
        {
            List<PricingRule> scprList = new List<PricingRule>() { };
            scList = db.ShoppingCartItems.ToList();
            foreach (var item in scList)
            {
                scprList.Add(db.PricingRules.Find(item.PricingRuleId));
            }
            return scprList;
        }
        //--------------------------------------
        //
        // GET: /ShoppingCart/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ShoppingCart/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShoppingCart/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ShoppingCart/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ShoppingCart/Delete/5

        public ActionResult Delete()
        {
            //The following code segment deletes all rows of a table, in this case 'Journal'
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            objCtx.ExecuteStoreCommand("TRUNCATE TABLE [ShoppingCartItem]");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /ShoppingCart/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
