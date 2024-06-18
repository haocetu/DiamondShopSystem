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
    public class ProductDiamondConfiguration : IEntityTypeConfiguration<ProductDiamond>
    {
        public void Configure(EntityTypeBuilder<ProductDiamond> builder)
        {
            builder.ToTable("ProductDiamonds");

            builder.HasKey(pd => pd.Id);

            builder.HasOne(pd => pd.Product).WithMany(p => p.ProductDiamonds).HasForeignKey(pd => pd.ProductId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pd => pd.Diamond).WithMany(d => d.ProductDiamonds).HasForeignKey(pd => pd.DiamondId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
