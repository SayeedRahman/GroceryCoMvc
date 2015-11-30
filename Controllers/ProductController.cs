using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.Controllers
{
    public class ProductController : Controller
    {
        protected UsersContext db = new UsersContext() { };

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            Product product = new Product() { };
            IEnumerable<DiscountRuleEnum> enumTypeList = Enum.GetValues(typeof(DiscountRuleEnum)).Cast<DiscountRuleEnum>();
            product.DiscountRuleEnumTypeSelectList = from s in enumTypeList
                                                select new SelectListItem
                                                {
                                                    Text = s.ToString(),
                                                    Value = ((int)s).ToString()
                                                };
            return View(product);
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.PricingRuleId = db.PricingRules.Where(p => p.DiscountRuleEnumType == product.DiscountRuleEnumType).FirstOrDefault().PricingRuleId;
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Product/Edit/5

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
        // GET: /Product/Delete/5

        public ActionResult Delete()
        {
            //The following code segment deletes all rows of a table, in this case 'Journal'
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            objCtx.ExecuteStoreCommand("TRUNCATE TABLE [Product]");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Product/Delete/5

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
