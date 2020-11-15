using System.ComponentModel.DataAnnotations;
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
        }
    }
}