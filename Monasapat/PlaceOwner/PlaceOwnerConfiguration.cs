using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class PlaceOwnerConfiguration : IEntityTypeConfiguration<PlaceOwner>
    {
        public void Configure(EntityTypeBuilder<PlaceOwner> builder)
        {
            builder.ToTable("PlaceOwner");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Name)
                .IsRequired().HasMaxLength(500);
        }
    }
}
