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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Property(p => p.PaymentType).IsRequired();

            builder.HasMany(p => p.Orders).WithOne(o => o.Payment).HasForeignKey(o => o.PaymentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
