using Domain.Entities;
using Infrastructures.FluentAPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Diamond> Diamonds { get; set; }
        public DbSet<ProductDiamond> ProductDiamonds { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string root = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            string apiDirectory = Path.Combine(root, "DiamondShopSystem(SWD392)");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiDirectory)
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new DiamondConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDiamondConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());

            //Role
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Customer"},
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "SaleStaff" },
                new Role { Id = 4, Name = "DeliveryStaff" }
                );
            //ProductType
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Material = "Gold", Weight = 3.75f, Price = 5567000},
                new ProductType { Id = 2, Material = "Platium", Weight = 1, Price = 827287 },
                new ProductType { Id = 3, Material = "Sliver", Weight = 1, Price = 22325 }
                );
            //Category
            modelBuilder.Entity<Category>().HasData(
                //Ring (size: 6 -> 20)
                new Category { Id = 1, Name = "Ring", Size = 6, Price = 200000, IsDeleted = false },
                new Category { Id = 2, Name = "Ring", Size = 7, Price = 250000, IsDeleted = false },
                new Category { Id = 3, Name = "Ring", Size = 8, Price = 300000, IsDeleted = false },
                new Category { Id = 4, Name = "Ring", Size = 9, Price = 350000, IsDeleted = false },
                new Category { Id = 5, Name = "Ring", Size = 10, Price = 400000, IsDeleted = false },
                new Category { Id = 6, Name = "Ring", Size = 11, Price = 450000, IsDeleted = false },
                new Category { Id = 7, Name = "Ring", Size = 12, Price = 500000, IsDeleted = false },
                new Category { Id = 8, Name = "Ring", Size = 13, Price = 550000, IsDeleted = false },
                new Category { Id = 9, Name = "Ring", Size = 14, Price = 600000, IsDeleted = false },
                new Category { Id = 10, Name = "Ring", Size = 15, Price = 650000, IsDeleted = false },
                new Category { Id = 11, Name = "Ring", Size = 16, Price = 700000, IsDeleted = false },
                new Category { Id = 12, Name = "Ring", Size = 17, Price = 750000, IsDeleted = false },
                new Category { Id = 13, Name = "Ring", Size = 18, Price = 800000, IsDeleted = false },
                new Category { Id = 14, Name = "Ring", Size = 19, Price = 850000, IsDeleted = false },
                new Category { Id = 15, Name = "Ring", Size = 20, Price = 900000, IsDeleted = false },
                //Necklace (length: 36 -> 60)
                new Category { Id = 16, Name = "Necklace", Length = 36, Price = 500000, IsDeleted = false },
                new Category { Id = 17, Name = "Necklace", Length = 38, Price = 550000, IsDeleted = false },
                new Category { Id = 18, Name = "Necklace", Length = 40, Price = 600000, IsDeleted = false },
                new Category { Id = 19, Name = "Necklace", Length = 42, Price = 650000, IsDeleted = false },
                new Category { Id = 20, Name = "Necklace", Length = 44, Price = 700000, IsDeleted = false },
                new Category { Id = 21, Name = "Necklace", Length = 46, Price = 750000, IsDeleted = false },
                new Category { Id = 22, Name = "Necklace", Length = 48, Price = 800000, IsDeleted = false },
                new Category { Id = 23, Name = "Necklace", Length = 50, Price = 850000, IsDeleted = false },
                new Category { Id = 24, Name = "Necklace", Length = 52, Price = 850000, IsDeleted = false },
                new Category { Id = 25, Name = "Necklace", Length = 54, Price = 900000, IsDeleted = false },
                new Category { Id = 26, Name = "Necklace", Length = 56, Price = 950000, IsDeleted = false },
                new Category { Id = 27, Name = "Necklace", Length = 58, Price = 1000000, IsDeleted = false },
                new Category { Id = 28, Name = "Necklace", Length = 60, Price = 1050000, IsDeleted = false },
                //Earring (no size, no length)
                new Category { Id = 29, Name = "Earring", IsDeleted = false },
                //Bracelet (size: 36 -> 60)
                new Category { Id = 30, Name = "Bracelet", Size = 36, Price = 500000, IsDeleted = false },
                new Category { Id = 31, Name = "Bracelet", Size = 38, Price = 550000, IsDeleted = false },
                new Category { Id = 32, Name = "Bracelet", Size = 40, Price = 600000, IsDeleted = false },
                new Category { Id = 33, Name = "Bracelet", Size = 42, Price = 650000, IsDeleted = false },
                new Category { Id = 34, Name = "Bracelet", Size = 44, Price = 700000, IsDeleted = false },
                new Category { Id = 35, Name = "Bracelet", Size = 46, Price = 750000, IsDeleted = false },
                new Category { Id = 36, Name = "Bracelet", Size = 48, Price = 800000, IsDeleted = false },
                new Category { Id = 37, Name = "Bracelet", Size = 50, Price = 850000, IsDeleted = false },
                new Category { Id = 38, Name = "Bracelet", Size = 52, Price = 900000, IsDeleted = false },
                new Category { Id = 39, Name = "Bracelet", Size = 54, Price = 950000, IsDeleted = false },
                new Category { Id = 40, Name = "Bracelet", Size = 56, Price = 1000000, IsDeleted = false },
                new Category { Id = 41, Name = "Bracelet", Size = 58, Price = 1050000, IsDeleted = false },
                new Category { Id = 42, Name = "Bracelet", Size = 60, Price = 1100000, IsDeleted = false },
                //Bangles (size 36 -> 42)
                new Category { Id = 43, Name = "Bangles", Length = 36, Price = 500000, IsDeleted = false },
                new Category { Id = 44, Name = "Bangles", Length = 38, Price = 550000, IsDeleted = false },
                new Category { Id = 45, Name = "Bangles", Length = 40, Price = 600000, IsDeleted = false },
                new Category { Id = 46, Name = "Bangles", Length = 42, Price = 650000, IsDeleted = false }
                );
        }
    }
}
