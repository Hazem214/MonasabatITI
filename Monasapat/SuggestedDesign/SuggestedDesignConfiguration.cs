using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class SuggestedDesignConfiguration : IEntityTypeConfiguration<SuggestedDesign>
    {
        public void Configure(EntityTypeBuilder<SuggestedDesign> builder)
        {
            builder.ToTable("SuggestedDesign");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
           
        }
    }
}
