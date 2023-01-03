using AppMVCBasic.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMVCBasic.Data.Context
{
    public class AspMVCBasicDbContext : DbContext
    {
        public AspMVCBasicDbContext(DbContextOptions options) : base (options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");                

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AspMVCBasicDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
