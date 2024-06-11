using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductTypes");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Material).IsRequired().HasMaxLength(50);

            builder.Property(pt => pt.Weight).IsRequired().HasColumnType("float");

            builder.Property(pt => pt.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasMany(pt => pt.Products).WithOne(p => p.ProductType).HasForeignKey(p => p.ProductTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
