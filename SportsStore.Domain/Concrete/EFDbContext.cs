using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().MapToStoredProcedures();
            modelBuilder.Entity<User>().MapToStoredProcedures();
            //modelBuilder.Entity<Media>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }
    }
    
    
}
