using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rest_api_custom_jwt_auth.Models;

namespace rest_api_custom_jwt_auth.EfConfigurations
{
    public class UserRoleEfConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("User_Role");

            builder.HasKey(ur => new
            {
                ur.IdRole,
                ur.IdUser
            }).HasName("UserRole_pk");

            builder.HasOne(ur => ur.IdUserNavigation)
                .WithMany(u => u.UserRoles)
                .HasConstraintName("User_UserRole")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(ur => ur.IdUser);

            builder.HasOne(ur => ur.IdRoleNavigation)
                .WithMany(r => r.UserRoles)
                .HasConstraintName("Role_UserRole")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(ur => ur.IdRole);
        }
    }
}
