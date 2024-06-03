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
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.DiscountPercentage).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.StartDate).IsRequired();

            builder.Property(p => p.EndDate).IsRequired();
        }
    }
}
