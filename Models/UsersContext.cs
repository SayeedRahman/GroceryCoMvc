using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;


namespace GroceryCoMvc.Models
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
            : base("name=DefaultConnection")
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<PricingRule> PricingRules { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

	public class GroceryCoDBInitializer : CreateDatabaseIfNotExists<UsersContext>
	{
	  protected override void Seed(UsersContext context)
	  {
		base.Seed(context);
	  }
	}    
}
