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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();

            builder.Property(a => a.Email).HasMaxLength(100).IsRequired();

            builder.Property(a => a.Password).HasMaxLength(100).IsRequired();

            builder.Property(a => a.Address).HasMaxLength(200);

            builder.Property(a => a.PhoneNumber).HasMaxLength(20);

            builder.Property(a => a.Gender).HasMaxLength(10);

            builder.Property(a => a.RoleId).IsRequired();

            builder.Property(a => a.Point).HasPrecision(18, 2);

            builder.Property(a => a.ConfirmationToken).HasMaxLength(100);

            builder.HasOne(a => a.Role).WithMany(r => r.Accounts).HasForeignKey(a => a.RoleId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Orders).WithOne(o => o.Account).HasForeignKey(o => o.AccountId).OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(a => a.Carts).WithOne(c => c.Account).HasForeignKey(o => o.AccountId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
