using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Configurations
{
    public class FilterViewMasterConfiguration : IEntityTypeConfiguration<FilterViewMaster>
    {
        public void Configure(EntityTypeBuilder<FilterViewMaster> builder)
        {
            builder.ToTable("FilterViewMaster");
            builder.Property(d => d.Name)
                .IsRequired();
            builder.Property(d => d.azureUserID)
                .IsRequired();

            builder.HasMany<FilterViewDetail>(wnd => wnd.FilterViewDetails)
                .WithOne(sblmnt => sblmnt.FilterViewMaster)
                .HasForeignKey(sblmnt => sblmnt.FilterViewMasterID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
