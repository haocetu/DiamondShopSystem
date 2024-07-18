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
        public DbSet<ProductWarranty> ProductWarranties { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

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
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());


            #region insert data
            //Role
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Customer"},
                new Role { Id = 2, Name = "Admin" },
                new Role { Id = 3, Name = "SaleStaff" },
                new Role { Id = 4, Name = "DeliveryStaff" }
                );
            //ProductType
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Material = "Vàng", Weight = 1, Price = 681000 },
                new ProductType { Id = 2, Material = "Bạch kim", Weight = 1, Price = 827287 },
                new ProductType { Id = 3, Material = "Bạc", Weight = 1, Price = 22325 }
                );
            //Category
            modelBuilder.Entity<Category>().HasData(
                //Ring (size: 6 -> 20)
                new Category { Id = 1, Name = "Nhẫn", Size = 6, IsDeleted = false },
                new Category { Id = 2, Name = "Nhẫn", Size = 7, IsDeleted = false },
                new Category { Id = 3, Name = "Nhẫn", Size = 8, IsDeleted = false },
                new Category { Id = 4, Name = "Nhẫn", Size = 9, IsDeleted = false },
                new Category { Id = 5, Name = "Nhẫn", Size = 10, IsDeleted = false },
                new Category { Id = 6, Name = "Nhẫn", Size = 11, IsDeleted = false },
                new Category { Id = 7, Name = "Nhẫn", Size = 12, IsDeleted = false },
                new Category { Id = 8, Name = "Nhẫn", Size  = 13, IsDeleted = false },
                new Category { Id = 9, Name = "Nhẫn", Size = 14, IsDeleted = false },
                new Category { Id = 10, Name = "Nhẫn", Size = 15, IsDeleted = false },
                new Category { Id = 11, Name = "Nhẫn", Size = 16, IsDeleted = false },
                new Category { Id = 12, Name = "Nhẫn", Size = 17, IsDeleted = false },
                new Category { Id = 13, Name = "Nhẫn", Size = 18, IsDeleted = false },
                new Category { Id = 14, Name = "Nhẫn", Size = 19, IsDeleted = false },
                new Category { Id = 15, Name = "Nhẫn", Size = 20, IsDeleted = false },
                //Necklace (length: 36 -> 60)
                new Category { Id = 16, Name = "Dây chuyền", Length = 36, IsDeleted = false },
                new Category { Id = 17, Name = "Dây chuyền", Length = 38, IsDeleted = false },
                new Category { Id = 18, Name = "Dây chuyền", Length = 40, IsDeleted = false },
                new Category { Id = 19, Name = "Dây chuyền", Length = 42, IsDeleted = false },
                new Category { Id = 20, Name = "Dây chuyền", Length = 44, IsDeleted = false },
                new Category { Id = 21, Name = "Dây chuyền", Length = 46, IsDeleted = false },
                new Category { Id = 22, Name = "Dây chuyền", Length = 48, IsDeleted = false },
                new Category { Id = 23, Name = "Dây chuyền", Length = 50, IsDeleted = false },
                new Category { Id = 24, Name = "Dây chuyền", Length = 52, IsDeleted = false },
                new Category { Id = 25, Name = "Dây chuyền", Length = 54, IsDeleted = false },
                new Category { Id = 26, Name = "Dây chuyền", Length = 56, IsDeleted = false },
                new Category { Id = 27, Name = "Dây chuyền", Length = 58, IsDeleted = false },
                new Category { Id = 28, Name = "Dây chuyền", Length = 60, IsDeleted = false },
                //Bracelet (size: 36 -> 60)
                new Category { Id = 29, Name = "Vòng tay", Size = 36, IsDeleted = false },
                new Category { Id = 30, Name = "Vòng tay", Size = 38, IsDeleted = false },
                new Category { Id = 31, Name = "Vòng tay", Size = 40, IsDeleted = false },
                new Category { Id = 32, Name = "Vòng tay", Size = 42, IsDeleted = false },
                new Category { Id = 33, Name = "Vòng tay", Size = 44, IsDeleted = false },
                new Category { Id = 34, Name = "Vòng tay", Size = 46, IsDeleted = false },
                new Category { Id = 35, Name = "Vòng tay", Size = 48, IsDeleted = false },
                new Category { Id = 36, Name = "Vòng tay", Size = 50, IsDeleted = false },
                new Category { Id = 37, Name = "Vòng tay", Size = 52, IsDeleted = false },
                new Category { Id = 38, Name = "Vòng tay", Size = 54, IsDeleted = false },
                new Category { Id = 39, Name = "Vòng tay", Size = 56, IsDeleted = false },
                new Category { Id = 40, Name = "Vòng tay", Size = 58, IsDeleted = false },
                new Category { Id = 41, Name = "Vòng tay", Size = 60, IsDeleted = false }
                );
            //Diamond
            modelBuilder.Entity<Diamond>().HasData(
                new Diamond { Id = 1, Name = "GIA Excellent FL ", Origin = "GIA", CaratWeight = 2.3f, Clarity = "FL", Cut = "Excellent", Color = "D", Price = 50000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 2, Name = "HRD VeryGood IF", Origin = "HRD", CaratWeight = 2.5f, Clarity = "IF", Cut = "VeryGood", Color = "E", Price = 60000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 3, Name = "IGI Good I1", Origin = "IGI", CaratWeight = 3.5f, Clarity = "I1", Cut = "Good", Color = "R", Price = 40000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 4, Name = "CGL Fair VVS1", Origin = "CGL", CaratWeight = 5f, Clarity = "VVS1", Cut = "Fair", Color = "S", Price = 70000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 5, Name = "EGL Excellent I3", Origin = "EGL", CaratWeight = 6.5f, Clarity = "I3", Cut = "Excellent", Color = "T", Price = 55000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 6, Name = "AGS VeryGood I2", Origin = "AGS", CaratWeight = 7.5f, Clarity = "I2", Cut = "VeryGood", Color = "X", Price = 60000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 7, Name = "CGL Good SI2", Origin = "CGL", CaratWeight = 5.3f, Clarity = "SI2", Cut = "Good", Color = "Y", Price = 65000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 8, Name = "EGL Fair I1", Origin = "EGL", CaratWeight = 4.7f, Clarity = "I1", Cut = "Fair", Color = "Z", Price = 80000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 9, Name = "AGS Excellent VS1", Origin = "AGS", CaratWeight = 2.7f, Clarity = "VS1", Cut = "Excellent", Color = "M", Price = 90000000, Quantity = 10, IsDeleted = false},
                new Diamond { Id = 10, Name = "IGI VeryGood VVS2", Origin = "IGI", CaratWeight = 6.3f, Clarity = "VVS2", Cut = "VeryGood", Color = "N", Price = 75000000, Quantity = 10, IsDeleted = false}
                );
            //Product
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Nhẫn Vàng trắng 14K đính đá  ",Quantity = 10, Weight = 7, CategoryId = 1, Price = 7236000, Wage = 500000, ProductTypeId = 1, IsDeleted = false },
                new Product { Id = 2, Name = "Dây chuyền Vàng Trắng Ý 18K ", Quantity = 10, Weight = 150, CategoryId = 16, Price = 15692000, Wage = 1000000, ProductTypeId = 1, IsDeleted = false },
                new Product { Id = 3, Name = "Lắc tay Bạc đính đá", Quantity = 10, Weight = 200, CategoryId = 3, Price = 700000, Wage = 100000, ProductTypeId = 3, IsDeleted = false }
                );
            //Product Diamond
            modelBuilder.Entity<ProductDiamond>().HasData(
                new ProductDiamond { Id = 1, ProductId = 1, DiamondId = 1, IsMain = true, IsDeleted = false},
                new ProductDiamond { Id = 2, ProductId = 2, DiamondId = 1, IsMain = true , IsDeleted = false},
                new ProductDiamond { Id = 3, ProductId = 2, DiamondId = 2, IsMain = false, IsDeleted = false },
                new ProductDiamond { Id = 4, ProductId = 2, DiamondId = 3, IsMain = false, IsDeleted = false },
                new ProductDiamond { Id = 5, ProductId = 3, DiamondId = 3, IsMain = true, IsDeleted = false }
                );
            //Promotion
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion { Id = 1, Point = 10000, DiscountPercentage = 0.5m, IsDeleted = false },
                new Promotion { Id = 2, Point = 15000, DiscountPercentage = 1, IsDeleted = false },
                new Promotion { Id = 3, Point = 20000, DiscountPercentage = 1.5m, IsDeleted = false });
            // Payment
            modelBuilder.Entity<Payment>().HasData(
                new Payment { Id = 1, PaymentMethod = "Payment in cash"}
                );
            #endregion
        }
    }
}
