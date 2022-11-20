using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class PlaceImageConfiguration : IEntityTypeConfiguration<PlaceImage>
    {
        public void Configure(EntityTypeBuilder<PlaceImage> builder)
        {
            builder.ToTable("PaceIMage");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Path)
                .IsRequired();
           
        }
    }
}
