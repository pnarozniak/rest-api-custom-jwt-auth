using Microsoft.EntityFrameworkCore;
using rest_api_custom_jwt_auth.EfConfigurations;
using rest_api_custom_jwt_auth.Models;

namespace rest_api_custom_jwt_auth.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEfConfiguration());
        }
    }
}
