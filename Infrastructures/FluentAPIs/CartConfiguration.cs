using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Account).WithMany(a => a.Carts).HasForeignKey(c => c.AccountId);

            builder.Property(p => p.TotalPrice).IsRequired();

            builder.HasMany(c => c.Items).WithOne(ci => ci.Cart).HasForeignKey(ci => ci.CartId);
        }
    }

}
