using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class DiamondConfiguration : IEntityTypeConfiguration<Diamond>
    {
        public void Configure(EntityTypeBuilder<Diamond> builder)
        {
            builder.ToTable("Diamonds");

            builder.HasKey(x => x.Id);

            builder.Property(d => d.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(d => d.Quantity).IsRequired();

            builder.Property(d => d.ImageUrl).HasMaxLength(500).IsRequired(false);

            builder.HasOne(d=>d.Claritys).WithMany(c=>c.Diamonds).HasForeignKey(d => d.ClarityId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d=>d.Origin).WithMany(c=>c.Diamonds).HasForeignKey(d => d.OriginId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d=>d.Cuts).WithMany(c=>c.Diamonds).HasForeignKey(d => d.CutId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d=>d.CaratWeight).WithMany(c=>c.Diamonds).HasForeignKey(d => d.CaratWeightId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(d=>d.Products).WithOne(p=>p.Diamond).HasForeignKey(p=>p.DiamondId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
