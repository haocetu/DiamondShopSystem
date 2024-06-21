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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(op => new { op.ProductId, op.OrderId });

            builder.Property(op => op.Quantity).IsRequired().HasColumnType("int");

            builder.Property(op => op.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(op => op.Order).WithMany(o => o.OrderItems).HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.Product).WithMany(p => p.OrderItems).HasForeignKey(op => op.ProductId);
        }
    }
}
