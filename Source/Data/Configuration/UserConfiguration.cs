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
            builder.Property(user => user.Phone)
                   .HasMaxLength(10)
                   .IsFixedLength();

            builder.Property(user => user.Email)
                   .IsRequired(false);
        }
    }
}