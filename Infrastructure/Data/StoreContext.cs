
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
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
                  var dateTimeprops = entitytype.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in props)
                    {
                        modelBuilder.Entity(entitytype.Name).Property(property.Name).HasConversion<double>();
                    }
                    foreach (var property in dateTimeprops)
                    {
                        modelBuilder.Entity(entitytype.Name).Property(property.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
