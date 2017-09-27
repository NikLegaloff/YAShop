using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Domain
{
    public class EfContext : DbContext
    {
        public EfContext() : base("YAShopDB") { }

        public DbSet<Product> Product { get; set; }
    }
}
