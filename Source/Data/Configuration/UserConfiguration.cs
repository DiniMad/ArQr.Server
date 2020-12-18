using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.PhoneNumber).IsUnique();

            builder.Property(user => user.PhoneNumber)
                   .HasMaxLength(10)
                   .IsFixedLength();

            User adminUser = new()
            {
                Id          = 1,
                PhoneNumber = "0000000000",
                // Plain Password is "Admin123"
                PasswordHash = "AQAAAAEAACcQAAAAELOXD5Z4lKhjCEQExaoM+Z0q7BR/vISBHA1XNP6nxJI2MrCD/x6vVCqEHCQ+mOHITg==",
                PhoneNumberConfirmed = true,
                Admin = true,
            };
            builder.HasData(adminUser);
        }
    }
}