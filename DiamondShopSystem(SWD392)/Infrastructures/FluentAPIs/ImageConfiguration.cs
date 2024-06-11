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
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.UrlPath).IsRequired().HasMaxLength(255);

            builder.HasOne(i => i.Product).WithMany(p => p.Images).HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Diamond).WithMany(d => d.Images).HasForeignKey(i => i.DiamondId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
