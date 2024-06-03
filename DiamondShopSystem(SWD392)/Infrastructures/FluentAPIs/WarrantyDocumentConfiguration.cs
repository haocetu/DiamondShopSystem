using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class WarrantyDocumentConfiguration : IEntityTypeConfiguration<WarrantyDocument>
    {
        public void Configure(EntityTypeBuilder<WarrantyDocument> builder)
        {
            builder.ToTable("WarrantyDocuments");

            builder.HasKey(x => x.Id);

            builder.Property(wd => wd.Period);

            builder.Property(wd => wd.TermsAndConditions).HasMaxLength(500);
        }
    }
}
