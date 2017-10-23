using System;
using System.Data.Entity;

namespace ImageStore.Domain
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
                        "Data Source=.;Initial Catalog=ImageStoreV1;Persist Security Info=True;User ID=sa;Password=Password1";
                return "Data Source=SPR\\SQLEXPRESS;Initial Catalog=ImageStoreV1;Integrated Security=True";
            }
        }

        public DbSet<Folder> Folder { get; set; }
        public DbSet<Image> Image { get; set; }
    }
}