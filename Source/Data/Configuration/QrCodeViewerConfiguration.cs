using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class QrCodeViewerConfiguration : IEntityTypeConfiguration<QrCodeViewer>
    {
        public void Configure(EntityTypeBuilder<QrCodeViewer> builder)
        {
            builder.Property(viewer => viewer.Id).ValueGeneratedNever();
        }
    }
}