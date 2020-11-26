using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(service => service.Id).ValueGeneratedOnAdd();
            builder.Property(service => service.Title).HasMaxLength(32);
            builder.Property(service => service.Description).HasMaxLength(128);
        }
    }
}