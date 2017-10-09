using System;
using System.Data.Entity;

namespace Sprut.MyShop.Domain
{
    public class EfContext : DbContext
    {
        public EfContext() : base(GetConnectionString)
        {
        }

        private static string GetConnectionString
        {
            get
            {
                if (Environment.MachineName == "KOT")
                    return
                        "Data Source=.;Initial Catalog=YAShopDBv1;Persist Security Info=True;User ID=sa;Password=Password1";
                return "Data Source=SPR\\SQLEXPRESS;Initial Catalog=YAShopDBv1;Integrated Security=True";
            }
        }

        //public EfContext() : base("d:\\Temp\\YAShopDBv1.mdf") { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}