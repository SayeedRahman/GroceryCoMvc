﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GroceryCoMvc.BLL;
using GroceryCoMvc.Models;

namespace GroceryCoMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static List<ShoppingCartItem> scList  { get; set; }
        public static List<ShoppingCartItem> gscList  { get; set; }
        public static List<PricingRule> prList  { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //------------------------------------------------
            scList = new List<ShoppingCartItem>() { };
            prList = new List<PricingRule>() { };
            gscList = new List<ShoppingCartItem>() { };
        }
    }
}