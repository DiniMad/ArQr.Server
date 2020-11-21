using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class QrCodeConfiguration : IEntityTypeConfiguration<QrCode>
    {
        public void Configure(EntityTypeBuilder<QrCode> builder)
        {
            builder.Property(code => code.Title).HasMaxLength(32);
            builder.Property(code => code.Description).HasMaxLength(128);
            builder.Property(code => code.AssociatedPhoneNumber).HasMaxLength(10).IsFixedLength();
            builder.Property(code => code.AssociatedWebsite).HasMaxLength(64);
        }
    }
}