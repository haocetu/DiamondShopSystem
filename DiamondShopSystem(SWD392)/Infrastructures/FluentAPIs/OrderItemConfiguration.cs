using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(op => op.Id);

            builder.Property(op => op.Quantity).IsRequired().HasColumnType("int");

            builder.Property(op => op.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(op => op.Order).WithMany(o => o.Items).HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.Product).WithMany(p => p.OrderItems).HasForeignKey(op => op.ProductId);
        }
    }
}
