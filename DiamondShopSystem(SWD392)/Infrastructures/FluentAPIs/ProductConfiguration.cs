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

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Size).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.Wage).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Quantity).IsRequired();

            builder.Property(p => p.CategoryId).IsRequired();

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.OrderProducts).WithOne(op => op.Product).HasForeignKey(op => op.ProductId).OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(p=>p.ProductType).WithMany(pt=>pt.Products).HasForeignKey(p=>p.ProductTypeId).OnDelete(DeleteBehavior.NoAction);
         
            builder.HasMany(p => p.ProductWarranties).WithOne(pw => pw.Product).HasForeignKey(pw => pw.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
