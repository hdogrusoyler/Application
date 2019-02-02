using System;
using System.Collections.Generic;
using System.Text;
using Application.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Blog.Entities.EFMappings
{
    public class PhotoMap : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(e => e.Path)
                .IsRequired();

            builder.HasOne(i => i.Content)
                .WithMany(i => i.Photos);
        }
    }
}
