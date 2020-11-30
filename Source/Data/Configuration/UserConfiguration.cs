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
            builder.HasIndex(user => user.Email).IsUnique();

            builder.Property(user => user.PhoneNumber)
                   .HasMaxLength(10)
                   .IsFixedLength();

            builder.Property(user => user.Email)
                   .IsRequired(false);

            User adminUser = new()
            {
                Id          = 1,
                PhoneNumber = "0000000000",
                // Plain Password is "Admin123"
                PasswordHash = "AQAAAAEAACcQAAAAELOXD5Z4lKhjCEQExaoM+Z0q7BR/vISBHA1XNP6nxJI2MrCD/x6vVCqEHCQ+mOHITg==",
                Email = "admin@arqr.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Admin = true,
            };
            builder.HasData(adminUser);
        }
    }
}