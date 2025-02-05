using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(u => u.User).WithMany(ur => ur.UserRoles).HasForeignKey(u => u.UserId);
            builder.HasOne(r => r.Role).WithMany(ur => ur.UserRoles).HasForeignKey(r => r.RoleId);
        }
    }
}
