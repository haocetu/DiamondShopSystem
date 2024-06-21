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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(o => o.Status).IsRequired();

            builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");

            builder.Property(o => o.ShipDate).IsRequired();

            builder.HasOne(o => o.Account).WithMany(a => a.Orders).HasForeignKey(o => o.AccountId).OnDelete(DeleteBehavior.NoAction);
         
            builder.HasOne(o => o.Payment).WithMany(a => a.Orders).HasForeignKey(o=>o.PaymentId).OnDelete(DeleteBehavior.NoAction);   
            
            builder.HasMany(o => o.ProductWarranties).WithOne(pw => pw.Order).HasForeignKey(o=>o.OrderId).OnDelete(DeleteBehavior.NoAction);   
        }
    }
}
