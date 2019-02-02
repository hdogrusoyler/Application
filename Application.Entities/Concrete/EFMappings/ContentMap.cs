using System;
using System.Collections.Generic;
using System.Text;
using Application.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Blog.Entities.EFMappings
{
    public class ContentMap : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Description)
                   .IsRequired();

            builder.HasMany(i => i.Photos)
                   .WithOne(i => i.Content)
                   .HasForeignKey(i => i.ContentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
