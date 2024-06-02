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
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProducts");

            builder.HasKey(op => new { op.ProductId, op.OrderId });

            builder.Property(op => op.Quantity).IsRequired();

            builder.Property(op => op.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(op => op.Order).WithMany(o => o.OrderProducts).HasForeignKey(op => op.OrderId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(op => op.Product).WithMany(o => o.OrderProducts).HasForeignKey(op => op.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
