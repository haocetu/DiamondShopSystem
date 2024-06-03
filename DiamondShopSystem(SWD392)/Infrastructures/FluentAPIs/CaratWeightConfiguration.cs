using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class CaratWeightConfiguration : IEntityTypeConfiguration<CaratWeight>
    {
        public void Configure(EntityTypeBuilder<CaratWeight> builder)
        {
            builder.ToTable("CaratWeights");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Weight).HasColumnType("float").IsRequired();

            builder.Property(c => c.Price).HasColumnType("decimal(18,2)").IsRequired();
            
            builder.HasMany(c => c.Diamonds).WithOne(d=>d.CaratWeight).HasForeignKey(d => d.CaratWeightId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
