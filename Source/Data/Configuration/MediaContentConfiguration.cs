using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class MediaContentConfiguration : IEntityTypeConfiguration<MediaContent>
    {
        public void Configure(EntityTypeBuilder<MediaContent> builder)
        {
            builder.Property(content => content.Extension).HasMaxLength(8).IsUnicode(false);
        }
    }
}