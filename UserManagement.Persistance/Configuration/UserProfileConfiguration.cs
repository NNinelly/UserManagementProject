using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistance.Configuration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(d => d.User)
                  .WithOne(d => d.UserProfile)
                  .HasForeignKey<UserProfile>(d => d.UserId);
        }
    }
}
