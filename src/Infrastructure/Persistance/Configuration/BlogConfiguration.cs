using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(250);
            builder.Property(c => c.Content).IsRequired();
            builder.Property(i => i.Image).IsRequired();
            builder.Property(ca => ca.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(ua => ua.UpdateAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.HasOne(u => u.User).WithMany(b => b.Blogs).HasForeignKey(ui => ui.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
