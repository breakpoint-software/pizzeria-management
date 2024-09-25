using Microsoft.EntityFrameworkCore;
using Models.Domain;


namespace Models
{

    public class PizzaManagementDbContext : DbContext
    {
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public PizzaManagementDbContext()
        {

        }
        public PizzaManagementDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 2, Name = "Margherita", Price = 5.0m },
                new Pizza { Id = 1, Name = "Ortolana", Price = 6.0m },
                new Pizza { Id = 3, Name = "Diavola", Price = 6.5m },
                new Pizza { Id = 4, Name = "Bufalina", Price = 7.0m }
            );
        }
    }


}
