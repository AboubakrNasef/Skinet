
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext:DbContext
    {
     

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entitytype in modelBuilder.Model.GetEntityTypes())
                {
                    var props = entitytype.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    foreach (var property in props)
                    {
                        modelBuilder.Entity(entitytype.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}
