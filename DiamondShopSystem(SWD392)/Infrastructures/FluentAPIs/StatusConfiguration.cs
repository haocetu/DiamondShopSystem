using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Statuses");

            builder.HasKey(x => x.Id);

            builder.Property(s => s.Name).IsRequired();

            builder.HasOne(s => s.Account).WithMany(a => a.Statuses).HasForeignKey(s => s.AccountId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(s => s.Orders).WithOne(o => o.Status).HasForeignKey(o => o.StatusId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
