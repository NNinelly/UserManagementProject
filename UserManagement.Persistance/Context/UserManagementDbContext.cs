using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;

namespace UserManagementProject.Context
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext()
        {

        }
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

    }
}
