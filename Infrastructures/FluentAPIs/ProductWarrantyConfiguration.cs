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
    public class ProductWarrantyConfiguration : IEntityTypeConfiguration<ProductWarranty>
    {
        public void Configure(EntityTypeBuilder<ProductWarranty> builder)
        {
            builder.ToTable("ProductWarranty");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate).IsRequired();

            builder.Property(x => x.EndDate).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.Product).WithMany(p => p.ProductWarranties).HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Order).WithMany(o => o.ProductWarranties).HasForeignKey(x => x.OrderId);
        }
    }
}
