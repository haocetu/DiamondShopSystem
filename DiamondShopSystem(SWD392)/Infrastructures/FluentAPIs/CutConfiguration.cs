using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class CutConfiguration : IEntityTypeConfiguration<Cut>
    {
        public void Configure(EntityTypeBuilder<Cut> builder)
        {
            builder.ToTable("Cuts");

            builder.Property(c => c.Name).HasMaxLength(100);

            builder.Property(c => c.Price).IsRequired();
                        
            builder.HasMany(c => c.Diamonds).WithOne(d=>d.Cuts).HasForeignKey(d => d.CutId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
