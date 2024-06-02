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
    public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedDate).HasColumnType("datetime2");

            builder.Property(e => e.CreatedBy).HasColumnType("nvarchar(255)").HasMaxLength(255);

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime2");

            builder.Property(e => e.ModifiedBy).HasColumnType("nvarchar(255)").HasMaxLength(255);

            builder.Property(e => e.DeletedDate).HasColumnType("datetime2");

            builder.Property(e => e.DeletedBy).HasColumnType("nvarchar(255)").HasMaxLength(255);

            builder.Property(e => e.IsDeleted).HasColumnType("bit").IsRequired();
        }
    }
}
