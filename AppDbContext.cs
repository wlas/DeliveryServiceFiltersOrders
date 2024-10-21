using DeliveryServiceFiltersOrders.Model;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceFiltersOrders
{
    public class DeliveryContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=./Data/delivery.db");
        }
    }
}
