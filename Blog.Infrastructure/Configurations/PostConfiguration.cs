using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Text).IsRequired();

            builder.Property(x => x.PostDate).IsRequired();
            builder.HasIndex(x => x.PostDate);
        }
    }
}