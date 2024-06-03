using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).HasMaxLength(200);

            builder.Property(p => p.Size).HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl).HasMaxLength(500);

            builder.Property(p => p.Quantity);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.Property(p => p.Wage).HasColumnType("decimal(18,2)");

            builder.HasOne(p=>p.Diamond).WithMany(d=>d.Products).HasForeignKey(p=>p.Id);

            builder.HasOne(p=>p.Category).WithMany(d=>d.Products).HasForeignKey(p=>p.CategoryId);

            builder.HasMany(p=>p.OrderProducts).WithOne(op=>op.Product).HasForeignKey(p=>p.ProductId);

            builder.HasOne(p=>p.WarrantyDocument).WithOne(wd=>wd.Products).HasForeignKey<Product>(p=>p.WarrantyDocumentsId).OnDelete(DeleteBehavior.NoAction);    
        }
    }
}
