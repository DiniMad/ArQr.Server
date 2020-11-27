using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class SupportedMediaExtensionConfiguration : IEntityTypeConfiguration<SupportedMediaExtension>
    {
        public void Configure(EntityTypeBuilder<SupportedMediaExtension> builder)
        {
            builder.Property(mediaExtension => mediaExtension.Id).ValueGeneratedOnAdd();
            builder.HasIndex(mediaExtension => mediaExtension.Extension).IsUnique();
            builder.Property(mediaExtension => mediaExtension.Extension).HasMaxLength(8).IsUnicode(false);
        }
    }
}