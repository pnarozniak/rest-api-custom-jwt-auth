using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rest_api_custom_jwt_auth.Models;

namespace rest_api_custom_jwt_auth.EfConfigurations
{
    public class RoleEfConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(r => r.IdRole)
                .HasName("Role_pk");

            builder.Property(r => r.IdRole)
                .UseIdentityColumn();

            builder.Property(r => r.Name).HasMaxLength(30).IsRequired();
        }
    }
}
