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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.Property(c => c.Size).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(c => c.Lenght).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(c => c.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasMany(c=>c.Products).WithOne(p=>p.Category).HasForeignKey(p=>p.Id);
        }
    }
}
