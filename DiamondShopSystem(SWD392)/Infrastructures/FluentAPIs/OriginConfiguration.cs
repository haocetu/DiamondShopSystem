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
    public class OriginConfiguration : IEntityTypeConfiguration<Origin>
    {
        public void Configure(EntityTypeBuilder<Origin> builder)
        {
            builder.ToTable("Origins");

            builder.Property(c => c.Name).HasMaxLength(100);

            builder.Property(c => c.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasMany(o => o.Diamonds).WithOne(d => d.Origin).HasForeignKey(d => d.OriginId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
