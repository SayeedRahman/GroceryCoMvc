using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryCoMvc.Models;

namespace GroceryCoMvc.Controllers
{
    public class PricingRuleController : Controller
    {
        public UsersContext db = new UsersContext() { };
        //
        // GET: /PricingRule/

        public ActionResult Index()
        {
            return View(db.PricingRules.ToList());
        }

        //
        // GET: /PricingRule/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PricingRule/Create

        public ActionResult Create()
        {
            PricingRule pr = new PricingRule();
            IEnumerable<DiscountRuleEnum> enumTypeList = Enum.GetValues(typeof(DiscountRuleEnum)).Cast<DiscountRuleEnum>();
            pr.DiscountRuleEnumTypeSelectList = from s in enumTypeList
                                                    select new SelectListItem
                                                    {
                                                        Text = s.ToString(),
                                                        Value = ((int)s).ToString()
                                                    };
            return View(pr);
        }

        //
        // POST: /PricingRule/Create

        [HttpPost]
        public ActionResult Create(PricingRule pr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pr.Name = pr.DiscountRuleEnumType.ToString();
                    db.PricingRules.Add(pr);
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
        // GET: /PricingRule/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PricingRule/Edit/5

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
        // GET: /PricingRule/Delete/5

        public ActionResult Delete()
        {
            //The following code segment deletes all rows of a table, in this case 'Journal'
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            objCtx.ExecuteStoreCommand("TRUNCATE TABLE [PricingRule]");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /PricingRule/Delete/5

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
