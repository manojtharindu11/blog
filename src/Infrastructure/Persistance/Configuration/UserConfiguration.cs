using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(n => n.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnType("varchar(250)");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n");

            builder.HasMany(x => x.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.User.Id);
        }
    }
}
