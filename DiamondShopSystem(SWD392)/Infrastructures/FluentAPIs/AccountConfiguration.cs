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
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.HasMany(u => u.Orders)
                    .WithOne(u => u.Account);    
            
            builder.HasMany(u => u.Statuses)
                    .WithOne(u => u.Account);

            builder.HasOne(u => u.Role)
                .WithMany(u => u.Accounts)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
