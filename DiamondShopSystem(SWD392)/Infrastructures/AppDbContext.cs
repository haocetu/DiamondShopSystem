using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<CaratWeight> CaratWeights { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Clarity> Clarities { get; set; }
        public DbSet<Cut> Cuts { get; set; }
        public DbSet<Diamond> Diamonds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<WarrantyDocument> WarrantyDocuments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
