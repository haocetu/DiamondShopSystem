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
    public class ClarityConfiguration : IEntityTypeConfiguration<Clarity>
    {
        public void Configure(EntityTypeBuilder<Clarity> builder)
        {
            builder.ToTable("Clarities");

            builder.Property(c => c.Name).HasMaxLength(100);

            builder.Property(c => c.Color).HasMaxLength(50);
                   
            builder.Property(c => c.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasMany(c => c.Diamonds).WithOne(d=>d.Claritys).HasForeignKey(d => d.ClarityId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
