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

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).HasMaxLength(100);

            builder.Property(d => d.Origin).HasMaxLength(100).IsRequired();

            builder.Property(d => d.CaratWeight).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(d => d.Clarity).HasMaxLength(50).IsRequired();

            builder.Property(d => d.Cut).HasMaxLength(50).IsRequired();

            builder.Property(d => d.Color).HasMaxLength(50).IsRequired();

            builder.Property(d => d.Price).HasColumnType("decimal(18,2)").IsRequired();

        }
    }
}
