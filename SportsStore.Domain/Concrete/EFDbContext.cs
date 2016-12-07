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
        public DbSet<WishListLine> WishListLines { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<OrderMainPart> Orders { get; set; }
        public DbSet<OrderExtension> OrderExtensions { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().MapToStoredProcedures();
            modelBuilder.Entity<User>().MapToStoredProcedures();
            modelBuilder.Entity<WishListLine>().MapToStoredProcedures();
            modelBuilder.Entity<CartLine>().MapToStoredProcedures();
            modelBuilder.Entity<OrderMainPart>().MapToStoredProcedures();
            modelBuilder.Entity<OrderExtension>().MapToStoredProcedures();
            modelBuilder.Entity<ShippingDetails>().MapToStoredProcedures();

            //modelBuilder.Entity<Media>().MapToStoredProcedures();

            base.OnModelCreating(modelBuilder);
        }
    }


}
