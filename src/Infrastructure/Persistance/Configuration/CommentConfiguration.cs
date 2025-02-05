using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Content).IsRequired();
            builder.Property(ca => ca.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(ua => ua.UpdateAt).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasOne(u => u.User).WithMany(c => c.Comments).HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Blog).WithMany(c => c.Comments).HasForeignKey(b => b.BlogId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
