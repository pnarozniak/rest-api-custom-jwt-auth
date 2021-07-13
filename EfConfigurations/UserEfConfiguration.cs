using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rest_api_custom_jwt_auth.Models;

namespace rest_api_custom_jwt_auth.EfConfigurations
{
    public class UserEfConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.IdUser)
                .HasName("User_pk");

            builder.Property(u => u.IdUser)
                .UseIdentityColumn();

            builder.Property(u => u.Email).HasMaxLength(70).IsRequired();

            builder.Property(u => u.HashedPassword).HasMaxLength(86).IsRequired();

            builder.Property(u => u.RefreshToken).HasMaxLength(36).IsRequired(false);

            builder.Property(u => u.RefreshTokenExpirationDate).IsRequired(false);
        }
    }
}
